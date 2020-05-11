namespace PcPartsPickerCrawler
{
    using AngleSharp;
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class NewEggCpuGatherer
    {
        public async Task<IEnumerable<string>> GatherCpuData()
        {
            var cpus = new List<RawCpu>();
            var productUrls = new List<string>();
            var parser = new HtmlParser();
            var client = new HttpClient();

            for (int page = 1; page <= 2; page++)
            {
                Console.Write($"{page} => ");

                var url = $"https://www.newegg.com/Processors-Desktops/SubCategory/ID-343/Page-{page}";
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
                
                var elements = document.GetElementsByClassName("item-container      ");
                
                if (elements.Length == 0)
                {
                    break;
                }
                
                foreach (var element in elements)
                {
                    string pcPartPickerUrl = null;
                    var options = element.InnerHtml.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var option in options)
                    {
                        if (option.Contains("href=") && option.Contains("item-img"))
                        {
                            // "product/9nm323/amd-ryzen-5-3600-36-thz-6-core-/processor-100-100000031box">\n"
                            var productUrlUntrimmed = option.Substring(option.IndexOf('/'));
                            var productUrl = productUrlUntrimmed.Substring(0, productUrlUntrimmed.Length - 19);
                            pcPartPickerUrl = "https:" + productUrl;
                            productUrls.Add(pcPartPickerUrl);
                        }
                    }
                }
            }
            foreach (var url in productUrls)
            {
                var response = await client.GetAsync(url);
                var htmlContent = await response.Content.ReadAsStringAsync();
                var document = await parser.ParseDocumentAsync(htmlContent);
                var productName = document.GetElementById("Specs");
            }
            
            return productUrls;
        }
    }

    public class RawCpu
    {
    }
}
