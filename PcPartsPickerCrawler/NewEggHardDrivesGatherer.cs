using AngleSharp.Html.Parser;
using NewEggCrawler.Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewEggCrawler
{
    public class NewEggHardDrivesGatherer
    {
        public async Task<IEnumerable<HardDiskDrive>> GatherHardDrivesData()
        {
            var videoCards = new List<HardDiskDrive>();
            var productUrls = new List<string>();
            var parser = new HtmlParser();
            var client = new HttpClient();

            for (int page = 1; page <= 100; page++)
            {
                Console.Write($"{page} => ");

                var url = $"https://www.newegg.com/Desktop-Memory/SubCategory/ID-14/Page-{page}";
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
                            var productUrlUntrimmed = option.Substring(option.IndexOf('/'));
                            var productUrl = productUrlUntrimmed.Substring(0, productUrlUntrimmed.Length - 19);
                            pcPartPickerUrl = "https:" + productUrl;
                            productUrls.Add(pcPartPickerUrl);
                        }
                    }
                }
            }

            int count = 0;

            foreach (var url in productUrls)
            {
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

                Console.WriteLine(count);
                count++;
                var document = await parser.ParseDocumentAsync(htmlContent);
                var manufacturerInfo = document.GetElementById("MfrContact");
                string productUrl = string.Empty;
                if (manufacturerInfo != null)
                {
                    productUrl = manufacturerInfo.GetElementsByTagName("a")[0].InnerHtml;
                }

                var productSpecs = document.GetElementById("detailSpecContent");
                string productSpecsInnerHtml = string.Empty;
                if (productSpecs == null)
                {
                    continue;
                }

                productSpecsInnerHtml = productSpecs.InnerHtml;
                var specs = productSpecsInnerHtml.Split("<dl>", StringSplitOptions.RemoveEmptyEntries);
                var productName = string.Empty;
                if (specs[0].Contains("<span>"))
                {
                    productName = specs[0].Substring(specs[0].IndexOf("<span>") + 6).Trim();
                    productName = productName.Substring(0, productName.IndexOf("</span>")).Trim();
                }

                var videoCard = new HardDiskDrive
                {
                    Name = productName,
                    ProductUrl = productUrl,
                };

                var imgHtmlElemnts = document.GetElementsByName("gallery");
                string imgHtml = string.Empty;
                if (imgHtmlElemnts.Length > 0)
                {
                    imgHtml = imgHtmlElemnts[0].InnerHtml;
                    imgHtml = imgHtml.Substring(imgHtml.IndexOf("//") + 2);
                    imgHtml = imgHtml.Substring(0, imgHtml.IndexOf(">") - 1);
                }

                if (imgHtml.Length > 0)
                {
                    videoCard.ImgUrl = imgHtml;
                }

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
                        switch (specName)
                        {
                            case "Brand":
                                videoCard.Brand = specValue;
                                break;
                            case "Interface":
                                videoCard.Interface = specValue;
                                break;
                            case "Capacity":
                                videoCard.Capacity = specValue;
                                break;
                            case "RPM":
                                videoCard.RPM = specValue;
                                break;
                            case "Cache":
                                videoCard.Cache = specValue;
                                break;
                            case "Usage":
                                videoCard.Usage = specValue;
                                break;
                            case "FormFactor":
                                videoCard.FormFactor = specValue;
                                break;

                            default:
                                break;
                        }
                    }
                }
                videoCards.Add(videoCard);
            }

            return videoCards;
        }
    }
}
