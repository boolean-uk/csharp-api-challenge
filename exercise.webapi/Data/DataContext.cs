using exercise.engine.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Programs.Add(new WashingProgram {Name = "Intensive 70", Duration = 150, WaterConsumption = 13.5M, ElectricityConsumption = 1.35M });
            Programs.Add(new WashingProgram { Name = "Eco 50", Duration = 60, WaterConsumption = 9M, ElectricityConsumption = 0.65M });
            Programs.Add(new WashingProgram {  Name = "Half Load", Duration = 40, WaterConsumption = 10.5M, ElectricityConsumption = 1.10M });
            Programs.Add(new WashingProgram { Name = "Clean Cycle", Duration = 55, WaterConsumption = 14, ElectricityConsumption = 1.45M });
            SaveChanges();
        }
        public DbSet<WashingProgram> Programs { get; set; }
        public DbSet<WashingProgramInstance> ProgramHistory { get; set; }
    }
}
