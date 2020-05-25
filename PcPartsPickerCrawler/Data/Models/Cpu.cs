namespace NewEggCrawler.Data.Models
{
    public class Cpu
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public byte[] Image { get; set; }

        public string Brand { get; set; }

        public string ProcesorType { get; set; }

        public string Series { get; set; }

        public string Model { get; set; }

        public string CPUSocketType { get; set; }

        public string NumberOfCores { get; set; }

        public string NumberOfThreads { get; set; }

        public string ManufacturingTech { get; set; }

        public string TDP { get; set; }
    }
}
