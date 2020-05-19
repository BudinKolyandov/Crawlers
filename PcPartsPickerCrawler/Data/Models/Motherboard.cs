namespace NewEggCrawler.Data.Models
{
    public class Motherboard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string CPUSocketType { get; set; }

        public string CPUType { get; set; }

        public string Chipset { get; set; }

        public string MemoryStandard { get; set; }

        public string NumberOfMemorySlots { get; set; }

        public string AudioChipset { get; set; }

        public string LANChipset { get; set; }

        public string MaxLANSpeed { get; set; }

        public string FormFactor { get; set; }
    }
}
