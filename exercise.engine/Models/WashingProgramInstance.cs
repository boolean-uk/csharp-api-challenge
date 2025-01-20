using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.engine.Models
{
    public class WashingProgramInstance
    {
        public int Id { get; set; }
        public WashingProgram Program { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsStopped { get; set; }
    }
}
