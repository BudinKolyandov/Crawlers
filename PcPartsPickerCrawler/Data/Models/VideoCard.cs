namespace NewEggCrawler.Data.Models
{
    public class VideoCard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public byte[] Image { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Interface { get; set; }

        public string GPU { get; set; }

        public string StreamProcessors { get; set; }

        public string DirectX { get; set; }

        public string OpenGL { get; set; }

        public string Cooler { get; set; }

        public string SystemRequirements { get; set; }

        public string FormFactor { get; set; }

        public string MaxGPULength { get; set; }

        public string SlotWidth { get; set; }
    }
}
