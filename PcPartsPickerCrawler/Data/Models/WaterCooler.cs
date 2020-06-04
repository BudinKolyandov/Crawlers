namespace NewEggCrawler.Data.Models
{
    public class WaterCooler
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductUrl { get; set; }

        public string ImgUrl { get; set; }

        public byte[] Image { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string BlockCompatibility { get; set; }

        public string PumpCapacity { get; set; }

        public string RadiatorDim { get; set; }

        public string FanSize { get; set; }

        public string FanRPM { get; set; }

        public string FanAirFlow { get; set; }

        public string TubeDim { get; set; }
    }
}
