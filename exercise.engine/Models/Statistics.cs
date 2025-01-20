using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.engine.Models
{
    public class Statistics
    {
        public decimal TotalWaterConsumtion { get; set; }
        public decimal TotalElectricityConsumption { get; set; }
        public decimal AverageWaterConsumption { get; set; }
        public decimal AverageElectricityConsumption { get; set; }

        public Statistics(List<WashingProgramInstance> washingProgramInstances)
        {
            TotalWaterConsumtion = washingProgramInstances.Where(p => !p.IsStopped).Sum(p => p.Program.WaterConsumption);
            TotalElectricityConsumption = washingProgramInstances.Where(p => !p.IsStopped).Sum(p => p.Program.ElectricityConsumption);
            AverageWaterConsumption = Math.Round(washingProgramInstances.Where(p => !p.IsStopped).Average(p => p.Program.WaterConsumption), 2);
            AverageElectricityConsumption = Math.Round(washingProgramInstances.Where(p => !p.IsStopped).Average(p => p.Program.ElectricityConsumption),2);
        }
    }
}
