using System;
using System.Linq;

namespace PcPartsPickerCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var urls = new NewEggCpuGatherer().GatherCpuData().GetAwaiter().GetResult();

            foreach (var url in urls)
            {
                Console.WriteLine(url.Name);
                foreach (var spec in url.Specs)
                {
                    Console.WriteLine(spec.Key + ' ' + spec.Value);
                }
            }
            Console.WriteLine(urls.Count());
        }
    }
}
