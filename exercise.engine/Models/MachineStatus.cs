using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.engine.Models
{
    public class MachineStatus
    {
        public WashingProgramInstance CurrentProgram { get; set; }
        public List<RefillWarning> RefillWarnings { get; set; }
    }
}
