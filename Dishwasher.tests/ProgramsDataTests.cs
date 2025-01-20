using NUnit.Framework;
using Dishwasher.engine;
using System.Linq;

namespace Dishwasher.Tests
{
    public class ProgramsDataTests
    {
        private DishwasherProgramsData _programsData;

        [SetUp]
        public void Setup()
        {
            _programsData = new DishwasherProgramsData();
        }

        [Test]
        public void Add_ShouldAddProgramAndReturnIt()
        {
            DishwasherProgram dishwasherProgram = new DishwasherProgram(_programsData.GetAll().Count() + 1, "Quick Wash", 8.0m, 0.5m, 1800);
            var program = _programsData.Add(dishwasherProgram);
            var allPrograms = _programsData.GetAll();

            Assert.That(allPrograms.Count(), Is.EqualTo(5));
            Assert.That(program.Id, Is.EqualTo(5));
            Assert.That(program.Name, Is.EqualTo("Quick Wash"));
            Assert.That(program.WaterConsumption, Is.EqualTo(8.0m));
            Assert.That(program.ElectricConsumption, Is.EqualTo(0.5m));
            Assert.That(program.Runtime, Is.EqualTo(1800));
        }

        [Test]
        public void GetAll_ShouldReturnAllPrograms()
        {
            var programs = _programsData.GetAll();

            Assert.That(programs.Count(), Is.EqualTo(4));
        }

        [Test]
        public void Get_ShouldReturnProgramById()
        {
            var program = _programsData.Get(2);

            Assert.That(program.Id, Is.EqualTo(2));
            Assert.That(program.Name, Is.EqualTo("Eco 50"));
        }

        [Test]
        public void Get_ShouldReturnNull()
        {
            var program = _programsData.Get(42);

            Assert.That(program, Is.Null);
        }
    }
}
