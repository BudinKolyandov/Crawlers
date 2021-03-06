﻿using NewEggCrawler;
using NewEggCrawler.Data;
using NewEggCrawler.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewEggPartsCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Cpu> cpuResult;
            IEnumerable<Memory> memoryResult;
            IEnumerable<Motherboard> motherboardResult;
            IEnumerable<VideoCard> videoCardResult;
            IEnumerable<Case> caseResult;
            IEnumerable<AirCooler> airCoolerResult;
            IEnumerable<WaterCooler> waterCoolerResult;
            IEnumerable<PSU> powerSuppliesResult;
            IEnumerable<HardDiskDrive> hardDrivesResult;
            IEnumerable<SolidStateDrive> solidStateDrivesResult;

            GatherData(out cpuResult, out memoryResult, out motherboardResult, out videoCardResult, out caseResult, out airCoolerResult, out waterCoolerResult, out powerSuppliesResult, out hardDrivesResult, out solidStateDrivesResult);
            
            SaveData(cpuResult, memoryResult, motherboardResult, videoCardResult, caseResult, airCoolerResult, waterCoolerResult, powerSuppliesResult, hardDrivesResult, solidStateDrivesResult);

            AddImagesToThePartsCpuToHdd();
            AddImagesToThePartsRest();
        }

        private static void AddImagesToThePartsRest()
        {
            using (var context = new ApplicationDbContext())
            {
                var memories = context.Memories.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var memoriesToAdd = new List<Memory>();
                Console.WriteLine(memories.Count);

                foreach (var memory in memories)
                {
                    Console.WriteLine(memory.Id);
                    var imgArray = TransformImage(memory.ImgUrl).GetAwaiter().GetResult();
                    var memoryToAdd = new Memory
                    {
                        Brand = memory.Brand,
                        CASLatency = memory.CASLatency,
                        Capacity = memory.Capacity,
                        HeatSpreader = memory.HeatSpreader,
                        Image = imgArray,
                        ImgUrl = memory.ImgUrl,
                        Model = memory.Model,
                        Name = memory.Name,
                        Series = memory.Series,
                        Speed = memory.Speed,
                        Timing = memory.Timing,
                        Type = memory.Type,
                        ProductUrl = memory.ProductUrl,
                    };
                    memoriesToAdd.Add(memoryToAdd);
                    memory.Image = imgArray;
                }
                context.Memories.RemoveRange(context.Memories);
                context.Memories.AddRange(memoriesToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                var motherboards = context.Motherboards.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var motherboardsToAdd = new List<Motherboard>();
                Console.WriteLine(motherboards.Count);

                foreach (var motherboard in motherboards)
                {
                    Console.WriteLine(motherboard.Id);
                    var imgArray = TransformImage(motherboard.ImgUrl).GetAwaiter().GetResult();
                    var motherboardToAdd = new Motherboard
                    {
                        Brand = motherboard.Brand,
                        AudioChipset = motherboard.AudioChipset,
                        LANChipset = motherboard.LANChipset,
                        Chipset = motherboard.Chipset,
                        CPUSocketType = motherboard.CPUSocketType,
                        CPUType = motherboard.CPUType,
                        FormFactor = motherboard.FormFactor,
                        Image = imgArray,
                        ImgUrl = motherboard.ImgUrl,
                        MaxLANSpeed = motherboard.MaxLANSpeed,
                        MemoryStandard = motherboard.MemoryStandard,
                        Model = motherboard.Model,
                        Name = motherboard.Name,
                        NumberOfMemorySlots = motherboard.NumberOfMemorySlots,
                        ProductUrl = motherboard.ProductUrl,
                    };
                    motherboardsToAdd.Add(motherboardToAdd);
                    motherboard.Image = imgArray;
                }
                context.Motherboards.RemoveRange(context.Motherboards);
                context.Motherboards.AddRange(motherboardsToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                var psus = context.PSUs.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var psusToAdd = new List<PSU>();
                Console.WriteLine(psus.Count);

                foreach (var psu in psus)
                {
                    Console.WriteLine(psu.Id);
                    var imgArray = TransformImage(psu.ImgUrl).GetAwaiter().GetResult();
                    var psuToAdd = new PSU
                    {
                        Brand = psu.Brand,
                        EnergyEfficiency = psu.EnergyEfficiency,
                        Fans = psu.Fans,
                        Image = imgArray,
                        ImgUrl = psu.ImgUrl,
                        MaximumPower = psu.MaximumPower,
                        Modular = psu.Modular,
                        Name = psu.Name,
                        Type = psu.Type,
                        ProductUrl = psu.ProductUrl,
                    };
                    psusToAdd.Add(psuToAdd);
                    psu.Image = imgArray;
                }
                context.PSUs.RemoveRange(context.PSUs);
                context.PSUs.AddRange(psusToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                var ssds = context.SolidStateDrives.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var ssdsToAdd = new List<SolidStateDrive>();
                Console.WriteLine(ssds.Count);

                foreach (var ssd in ssds)
                {
                    Console.WriteLine(ssd.Id);
                    var imgArray = TransformImage(ssd.ImgUrl).GetAwaiter().GetResult();
                    var ssdToAdd = new SolidStateDrive
                    {
                        Brand = ssd.Brand,
                        Capacity = ssd.Capacity,
                        FormFactor = ssd.FormFactor,
                        DeviceType = ssd.DeviceType,
                        Image = imgArray,
                        ImgUrl = ssd.ImgUrl,
                        Interface = ssd.Interface,
                        MemoryComponents = ssd.MemoryComponents,
                        Name = ssd.Name,
                        UsedFor = ssd.UsedFor,
                        ProductUrl = ssd.ProductUrl,
                    };
                    ssdsToAdd.Add(ssdToAdd);
                    ssd.Image = imgArray;
                }
                context.SolidStateDrives.RemoveRange(context.SolidStateDrives);
                context.SolidStateDrives.AddRange(ssdsToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                var videoCards = context.VideoCards.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var videoCardsToAdd = new List<VideoCard>();
                Console.WriteLine(videoCards.Count);

                foreach (var videoCard in videoCards)
                {
                    Console.WriteLine(videoCard.Id);
                    var imgArray = TransformImage(videoCard.ImgUrl).GetAwaiter().GetResult();
                    var videoCordToAdd = new VideoCard
                    {
                        Brand = videoCard.Brand,
                        Cooler = videoCard.Cooler,
                        DirectX = videoCard.DirectX,
                        FormFactor = videoCard.FormFactor,
                        GPU = videoCard.GPU,
                        Image = imgArray,
                        ImgUrl = videoCard.ImgUrl,
                        Interface = videoCard.Interface,
                        MaxGPULength = videoCard.MaxGPULength,
                        Model = videoCard.Model,
                        Name = videoCard.Name,
                        OpenGL = videoCard.OpenGL,
                        SlotWidth = videoCard.SlotWidth,
                        StreamProcessors = videoCard.StreamProcessors,
                        SystemRequirements = videoCard.SystemRequirements,
                        ProductUrl = videoCard.ProductUrl,
                    };
                    videoCardsToAdd.Add(videoCordToAdd);
                    videoCard.Image = imgArray;
                }
                context.VideoCards.RemoveRange(context.VideoCards);
                context.VideoCards.AddRange(videoCardsToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                var waterCoolers = context.WaterCoolers.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var waterCoolersToAdd = new List<WaterCooler>();
                Console.WriteLine(waterCoolers.Count);

                foreach (var waterCooler in waterCoolers)
                {
                    Console.WriteLine(waterCooler.Id);
                    var imgArray = TransformImage(waterCooler.ImgUrl).GetAwaiter().GetResult();
                    var waterCoolerToAdd = new WaterCooler
                    {
                        Brand = waterCooler.Brand,
                        FanAirFlow = waterCooler.FanAirFlow,
                        BlockCompatibility = waterCooler.BlockCompatibility,
                        FanRPM = waterCooler.FanRPM,
                        FanSize = waterCooler.FanSize,
                        Image = imgArray,
                        ImgUrl = waterCooler.ImgUrl,
                        Name = waterCooler.Name,
                        PumpCapacity = waterCooler.PumpCapacity,
                        RadiatorDim = waterCooler.RadiatorDim,
                        TubeDim = waterCooler.TubeDim,
                        Type = waterCooler.Type,
                        ProductUrl = waterCooler.ProductUrl,
                    };
                    waterCoolersToAdd.Add(waterCoolerToAdd);
                    waterCooler.Image = imgArray;
                }
                context.WaterCoolers.RemoveRange(context.WaterCoolers);
                context.WaterCoolers.AddRange(waterCoolersToAdd);
                context.SaveChanges();
            }
        }

        private static void AddImagesToThePartsCpuToHdd()
        {
            using (var context = new ApplicationDbContext())
            {
                var cpus = context.Cpus
                    .Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var cpusToAdd = new List<Cpu>();
                Console.WriteLine(cpus.Count);

                foreach (var cpu in cpus)
                {
                    Console.WriteLine(cpu.Id);
                    var imgArray = TransformImage(cpu.ImgUrl).GetAwaiter().GetResult();
                    var cpuToAdd = new Cpu
                    {
                        Brand = cpu.Brand,
                        CPUSocketType = cpu.CPUSocketType,
                        Image = imgArray,
                        ImgUrl = cpu.ImgUrl,
                        ManufacturingTech = cpu.ManufacturingTech,
                        Model = cpu.Model,
                        Name = cpu.Name,
                        NumberOfCores = cpu.NumberOfCores,
                        NumberOfThreads = cpu.NumberOfThreads,
                        ProcesorType = cpu.ProcesorType,
                        TDP = cpu.TDP,
                        Series = cpu.Series,
                        ProductUrl = cpu.ProductUrl,
                    };
                    cpusToAdd.Add(cpuToAdd);
                }
                context.Cpus.RemoveRange(context.Cpus);
                context.Cpus.AddRange(cpusToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                var airCoolers = context.AirCoolers.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var airCoolersToAdd = new List<AirCooler>();
                Console.WriteLine(airCoolers.Count);

                foreach (var airCooler in airCoolers)
                {
                    Console.WriteLine(airCooler.Id);
                    var imgArray = TransformImage(airCooler.ImgUrl).GetAwaiter().GetResult();
                    var airCoolerToAdd = new AirCooler
                    {
                        Brand = airCooler.Brand,
                        FanSize = airCooler.FanSize,
                        CPUSocketCompatibility = airCooler.CPUSocketCompatibility,
                        HeatsinkDimensions = airCooler.HeatsinkDimensions,
                        Image = imgArray,
                        ImgUrl = airCooler.ImgUrl,
                        MaxCPUCoolerHeight = airCooler.MaxCPUCoolerHeight,
                        Name = airCooler.Name,
                        PowerConnector = airCooler.PowerConnector,
                        RPM = airCooler.RPM,
                        Weight = airCooler.Weight,
                        ProductUrl = airCooler.ProductUrl,
                    };
                    airCoolersToAdd.Add(airCoolerToAdd);
                }
                context.AirCoolers.RemoveRange(context.AirCoolers);
                context.AirCoolers.AddRange(airCoolersToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                var cases = context.Cases.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var casesToAdd = new List<Case>();
                Console.WriteLine(cases.Count);

                foreach (var @case in cases)
                {
                    Console.WriteLine(@case.Id);
                    var imgArray = TransformImage(@case.ImgUrl).GetAwaiter().GetResult();
                    var caseToAdd = new Case
                    {
                        Brand = @case.Brand,
                        MaxCPUCoolerHeightAllowance = @case.MaxCPUCoolerHeightAllowance,
                        MaxGPULengthAllowance = @case.MaxGPULengthAllowance,
                        CaseMaterial = @case.CaseMaterial,
                        Color = @case.Color,
                        ExpansionSlots = @case.ExpansionSlots,
                        Image = imgArray,
                        ImgUrl = @case.ImgUrl,
                        MotherboardCompatibility = @case.MotherboardCompatibility,
                        Name = @case.Name,
                        SidePanelWindow = @case.SidePanelWindow,
                        Type = @case.Type,
                        ProductUrl = @case.ProductUrl,
                    };
                    casesToAdd.Add(caseToAdd);
                    @case.Image = imgArray;
                }
                context.Cases.RemoveRange(context.Cases);
                context.Cases.AddRange(casesToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext())
            {
                var hdds = context.HardDiskDrives.Where(x => x.Name != " ").ToList();
                context.SaveChanges();
                var hardDisksToAdd = new List<HardDiskDrive>();
                Console.WriteLine(hdds.Count);

                foreach (var hdd in hdds)
                {
                    Console.WriteLine(hdd.Id);
                    var imgArray = TransformImage(hdd.ImgUrl).GetAwaiter().GetResult();
                    var hardToAdd = new HardDiskDrive
                    {
                        Brand = hdd.Brand,
                        Cache = hdd.Cache,
                        Capacity = hdd.Capacity,
                        FormFactor = hdd.FormFactor,
                        Image = imgArray,
                        ImgUrl = hdd.ImgUrl,
                        Interface = hdd.Interface,
                        Name = hdd.Name,
                        RPM = hdd.RPM,
                        Usage = hdd.Usage,
                        ProductUrl = hdd.ProductUrl,
                    };
                    hardDisksToAdd.Add(hardToAdd);
                    hdd.Image = imgArray;
                }
                context.HardDiskDrives.RemoveRange(context.HardDiskDrives);
                context.HardDiskDrives.AddRange(hardDisksToAdd);
                context.SaveChanges();
            }
        }

        private static async Task<byte[]> TransformImage(string imgUrl)
        {
            var img = new byte[5000];

            var fullUrl = @"https://" + imgUrl;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(fullUrl))
                {
                    using (var source = await response.Content.ReadAsStreamAsync())
                    {
                        byte[] buffer = new byte[16 * 1024];
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int read;
                            while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                            img = ms.ToArray();
                        }
                    }
                }
            }

            return img;
        }

        private static void SaveData(IEnumerable<Cpu> cpuResult, IEnumerable<Memory> memoryResult, IEnumerable<Motherboard> motherboardResult, IEnumerable<VideoCard> videoCardResult, IEnumerable<Case> caseResult, IEnumerable<AirCooler> airCoolerResult, IEnumerable<WaterCooler> waterCoolerResult, IEnumerable<PSU> powerSuppliesResult, IEnumerable<HardDiskDrive> hardDrivesResult, IEnumerable<SolidStateDrive> solidStateDrivesResult)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Cpus.AddRange(cpuResult);
                context.Memories.AddRange(memoryResult);
                context.Motherboards.AddRange(motherboardResult);
                context.VideoCards.AddRange(videoCardResult);
                context.Cases.AddRange(caseResult);
                context.AirCoolers.AddRange(airCoolerResult);
                context.WaterCoolers.AddRange(waterCoolerResult);
                context.PSUs.AddRange(powerSuppliesResult);
                context.HardDiskDrives.AddRange(hardDrivesResult);
                context.SolidStateDrives.AddRange(solidStateDrivesResult);
                context.SaveChanges();
            }
        }

        private static void GatherData(out IEnumerable<Cpu> cpuResult, out IEnumerable<Memory> memoryResult, out IEnumerable<Motherboard> motherboardResult, out IEnumerable<VideoCard> videoCardResult, out IEnumerable<Case> caseResult, out IEnumerable<AirCooler> airCoolerResult, out IEnumerable<WaterCooler> waterCoolerResult, out IEnumerable<PSU> powerSuppliesResult, out IEnumerable<HardDiskDrive> hardDrivesResult, out IEnumerable<SolidStateDrive> solidStateDrivesResult)
        {
            cpuResult = new NewEggCpuGatherer().GatherCpuData().GetAwaiter().GetResult();
            memoryResult = new NewEggMemoryGatherer().GatherMemoryData().GetAwaiter().GetResult();
            motherboardResult = new NewEggMotherboardGatherer().GatherMotherboardData().GetAwaiter().GetResult();
            videoCardResult = new NewEggVideoCardGatherer().GatherVideoCardData().GetAwaiter().GetResult();
            caseResult = new NewEggCaseGatherer().GatherVideoCardData().GetAwaiter().GetResult();
            airCoolerResult = new NewEggAirCoolerGatherer().GatherAirCoolerData().GetAwaiter().GetResult();
            waterCoolerResult = new NewEggWaterCoolerGatherer().GatherWaterCoolerData().GetAwaiter().GetResult();
            powerSuppliesResult = new NewEggPowerSupplyGatherer().GatherPowerSuppliesData().GetAwaiter().GetResult();
            hardDrivesResult = new NewEggHardDrivesGatherer().GatherHardDrivesData().GetAwaiter().GetResult();
            solidStateDrivesResult = new NewEggSolidStateDrivesGatherer().GatherSolidStateDrivesData().GetAwaiter().GetResult();
        }
     }
}
