using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace ArdesCrawler
{
    public class ArdesBgDataGatherer
    {
        public async Task<IEnumerable<RawProduct>> GatherData(string name, int pages)
        {
            var products = new List<RawProduct>();
            var productUrls = new List<string>();
            var parser = new HtmlParser();
            var client = new HttpClient();

            for (int page = pages; page >= 1; page--)
            {
                Console.Write($"{page} => ");

                var url = $"https://ardes.bg/komponenti/{name}/page/{page}";
                string htmlContent = null;
                for (var i = 0; i < 10; i++)
                {
                    try
                    {
                        var response = await client.GetAsync(url);
                        htmlContent = await response.Content.ReadAsStringAsync();
                        break;
                    }
                    catch
                    {
                        Console.Write('!');
                        Thread.Sleep(500);
                    }
                }

                if (string.IsNullOrWhiteSpace(htmlContent))
                {
                    break;
                }

                var document = await parser.ParseDocumentAsync(htmlContent);

                var elements = document.GetElementsByClassName("product clearfix");

                if (elements.Length == 0)
                {
                    break;
                }

                foreach (var element in elements)
                {
                    string ardesUrl = null;
                    var options = element.InnerHtml.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var option in options)
                    {
                        if (option.Contains("href="))
                        {
                            // "/product/2tb-adata-hv620s-ahv620s-2tu3-cbk-103788\">\n"
                            var productUrlUntrimmed = option.Substring(option.IndexOf('/'));
                            var productUrl = productUrlUntrimmed.Substring(0, productUrlUntrimmed.Length - 3);
                            ardesUrl = "https://ardes.bg" + productUrl;
                            productUrls.Add(ardesUrl);
                        }
                    }
                }
            }
            foreach (var url in productUrls)
            {
                var response = await client.GetAsync(url);
                var htmlContent = await response.Content.ReadAsStringAsync();
                var document = await parser.ParseDocumentAsync(htmlContent);
                var productName = document.QuerySelector(".product-title")?.TextContent.Trim();
                if (productName.Contains('-'))
                {
                    productName = productName.Substring(0, productName.IndexOf('-'));
                }
                var imgUrlInnerHtml = document.QuerySelector(".slide-item").InnerHtml;
                var imgurlFirstPart = imgUrlInnerHtml.Substring(imgUrlInnerHtml.IndexOf("/")).Trim();
                var imgUrl = "https://ardes.bg" + imgurlFirstPart.Substring(0, imgurlFirstPart.IndexOf('\"'));

                var elements = document.GetElementsByClassName("tech-specs-list");
                if (elements.Length == 0)
                {
                    break;
                }
                foreach (var element in elements)
                {
                    string capacity = null;
                    string interfaceType = null;
                    string type = null;
                    var results = element.InnerHtml.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    var typeRaw = results[1];
                    if (typeRaw.Contains('<'))
                    {
                        var typeFirst = typeRaw.Substring(0, typeRaw.LastIndexOf('<'));
                        if (typeFirst.Contains('>'))
                        {
                            type = typeFirst.Substring(typeFirst.LastIndexOf('>') + 2);
                        }
                    }
                    foreach (var result in results)
                    {
                        if (result.Contains("Капацитет") && !results.Contains("-"))
                        {
                            var capacityRaw = result;
                            var capacityFirst = capacityRaw.Substring(0, capacityRaw.LastIndexOf('<'));
                            capacity = capacityFirst.Substring(capacityFirst.LastIndexOf('>') + 2);
                        }
                        else if(result.Contains("Интерфейс"))
                        {
                            var interfaceRaw = result;
                            var interfaceFirst = interfaceRaw.Substring(0, interfaceRaw.LastIndexOf('<'));
                            interfaceType = interfaceFirst.Substring(interfaceFirst.LastIndexOf('>') + 2);
                        }
                    }

                    var product = new RawProduct
                    {
                        Name = productName,
                        ImgUrl = imgUrl,
                        Type = type,
                        Capacity = capacity,
                        Interface = interfaceType,
                    };
                    products.Add(product);
                }
            }
            Console.WriteLine($"OK. {products.Count} total product(s).");

            return products;
        }
    }

    public class RawProduct
    {
        public string ImgUrl { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Capacity { get; set; }

        public string Interface { get; set; }
    }
}

