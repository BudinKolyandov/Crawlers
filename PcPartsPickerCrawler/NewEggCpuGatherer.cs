﻿namespace PcPartsPickerCrawler
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
        public async Task<IEnumerable<RawCpu>> GatherCpuData()
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
                var productSpecs = document.GetElementById("detailSpecContent").InnerHtml;
                var specs = productSpecs.Split("<dl>", StringSplitOptions.RemoveEmptyEntries);
                var productName = string.Empty;
                if (specs[0].Contains("<span>"))
                {
                    productName = specs[0].Substring(specs[0].IndexOf("<span>") + 6).Trim();
                    productName = productName.Substring(0, productName.IndexOf("</span>")).Trim();
                }
                var cpu = new RawCpu
                {
                    Name = productName,
                    Specs = new Dictionary<string, string>(),
                };

                foreach (var spec in specs)
                {
                    if (spec.Contains("<dt>") && spec.Contains("<dd>"))
                    {
                        var replaced = spec.Replace("<dt>", "|");
                        replaced = replaced.Replace("</dt>", "|");
                        replaced = replaced.Replace("<dd>", "|");
                        replaced = replaced.Replace("</dd>", "|");
                        var specsList = replaced.Split('|', StringSplitOptions.RemoveEmptyEntries);
                        var specName = specsList[0];
                        var specValue = specsList[1];
                        if (specName.Contains("a data"))
                        {
                            specName = specName.Substring(specName.IndexOf(">") + 1);
                            specName = specName.Substring(0, specName.IndexOf("<"));
                        }
                        cpu.Specs.Add(specName, specValue);
                    }
                }
                cpus.Add(cpu);
            }
            
            return cpus;
        }
    }

    public class RawCpu
    {
        public string Name { get; set; }

        public Dictionary<string, string> Specs { get; set; }
    }
}
