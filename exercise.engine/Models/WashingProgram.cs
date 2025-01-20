using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.engine.Models
{
    public class WashingProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal WaterConsumption { get; set; }
        public decimal ElectricityConsumption { get; set; }
        public int Duration { get; set; }
    }
}
