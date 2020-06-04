namespace NewEggCrawler.Data.Models
{
    public class PSU
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductUrl { get; set; }

        public string ImgUrl { get; set; }

        public byte[] Image { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string MaximumPower { get; set; }

        public string Fans { get; set; }

        public string Modular { get; set; }

        public string EnergyEfficiency { get; set; }
    }
}
