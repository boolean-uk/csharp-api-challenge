using exercise.engine.Models;

namespace exercise.tests
{
    public class Tests
    {
        DishWasher dishWasher;
        List<WashingProgram> Programs = new List<WashingProgram>();
        
        [SetUp]
        public void Setup()
        {
            dishWasher = new DishWasher();
            Programs.Add(new WashingProgram { Name = "Intensive 70", Duration = 150, WaterConsumption = 13.5M, ElectricityConsumption = 1.35M });
            Programs.Add(new WashingProgram { Name = "Eco 50", Duration = 60, WaterConsumption = 9M, ElectricityConsumption = 0.65M });
            Programs.Add(new WashingProgram { Name = "Half Load", Duration = 40, WaterConsumption = 10.5M, ElectricityConsumption = 1.10M });
            Programs.Add(new WashingProgram { Name = "Clean Cycle", Duration = 55, WaterConsumption = 14, ElectricityConsumption = 1.45M });
        }

        [Test]
        public void TestStartProgramSubtractResource()
        {
            dishWasher.Start(Programs[0]);
            Assert.That(dishWasher.Tablets, Is.EqualTo(62));
            Assert.That(dishWasher.Salt, Is.LessThan(3));
            Assert.That(dishWasher.RinseAid, Is.LessThan(1));
            Assert.That(dishWasher.ProgramInstances.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestStopProgram()
        {
            dishWasher.Start(Programs[0]);
            dishWasher.Stop();
            Assert.That(dishWasher.ProgramInstances.Last().IsStopped, Is.True);
        }

        [Test]
        public void TestGetConsumption()
        {
            Assert.That(dishWasher.GetConsumption(Programs[0], 60), Is.EqualTo(0.225));
        }

    

        [Test]
        public void TestTotalWaterEnergyConsumption()
        {
            dishWasher.Start(Programs[0]);
            dishWasher.Start(Programs[1]);
            dishWasher.Start(Programs[2]);

            Assert.That(dishWasher.GetStatistics(dishWasher.ProgramInstances).TotalElectricityConsumption, Is.EqualTo(3.1));
            Assert.That(dishWasher.GetStatistics(dishWasher.ProgramInstances).TotalWaterConsumtion, Is.EqualTo(33));
        }

        [Test]
        public void TestAverageWaterEnergyConsumption()
        {
            dishWasher.Start(Programs[0]);
            dishWasher.Start(Programs[1]);
            dishWasher.Start(Programs[2]);
            Statistics statistics = dishWasher.GetStatistics(dishWasher.ProgramInstances);

            Assert.That(statistics.AverageElectricityConsumption, Is.EqualTo(1.03));
            Assert.That(statistics.AverageWaterConsumption, Is.EqualTo(11));
        }

        [Test]
        public void TestTabletsRefill()
        {
            dishWasher.Tablets = 0;
            dishWasher.Start(Programs[0]);
            Assert.That(dishWasher.Tablets, Is.EqualTo(62));
        }
        [Test]
        public void TestSaltRinseAidRefill()
        {
            dishWasher.Salt = 0;
            dishWasher.RinseAid = 0;
            dishWasher.Start(Programs[0]);
            Assert.That(dishWasher.Salt, Is.EqualTo(2.775));
            Assert.That(dishWasher.RinseAid, Is.EqualTo(0.775));
        }
        [Test]
        public void TestTabletRefillWarning()
        {
            dishWasher.Tablets = 3;
            dishWasher.Start(Programs[0]);
            int tabletWarnings = dishWasher.GetCurrentProgram().RefillWarnings.Where(x => x.Message.ToLower().Contains("tablet")).Count();
            Assert.That(tabletWarnings, Is.EqualTo(1));
        }

        [Test]
        public void TestSaltRefillWarning()
        {
            dishWasher.Salt = 0.2;
            dishWasher.Start(Programs[0]);
            int saltWarnings = dishWasher.GetCurrentProgram().RefillWarnings.Where(x => x.Message.ToLower().Contains("salt")).Count();
            Assert.That(saltWarnings, Is.EqualTo(1));
        }
        [Test]
        public void TestRinseAidRefillWarning()
        {
            dishWasher.RinseAid = 0.1;
            dishWasher.Start(Programs[0]);
            int rinseAidWarningsCount = dishWasher.GetCurrentProgram().RefillWarnings.Where(x => x.Message.ToLower().Contains("rinse aid")).Count();
            Assert.That(rinseAidWarningsCount, Is.EqualTo(1));
        }

        [Test]
        public void TestCleanNotifier()
        {
            dishWasher.Start(Programs[3]);
            Assert.That(dishWasher.LastCleanProgram.Second, Is.EqualTo(DateTime.Now.Second));
            for (int i = 0; i < 52; i++)
            {
                dishWasher.Start(Programs[1]);//60 minute program
            }
            int cleanWarning = dishWasher.GetCurrentProgram().RefillWarnings.Where(x => x.Message.ToLower().Contains("clean")).Count();
            Assert.That(cleanWarning, Is.EqualTo(1));
            dishWasher.Start(Programs[3]);
            for (int i = 0; i < 48; i++)
            {
                dishWasher.Start(Programs[1]);
            }
            cleanWarning = dishWasher.GetCurrentProgram().RefillWarnings.Where(x => x.Message.ToLower().Contains("clean")).Count();
            Assert.That(cleanWarning, Is.EqualTo(0));
        }
    }
}
