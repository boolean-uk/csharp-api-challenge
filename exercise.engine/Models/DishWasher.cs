using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.engine.Models
{
    public class DishWasher
    {
        public int Tablets { get; set; }
        public double Salt { get; set; }
        public double RinseAid { get; set; }
        public List<WashingProgramInstance> ProgramInstances { get; set; } = new List<WashingProgramInstance>();
        public DateTime LastCleanProgram { get; set; }
        private const int MaxTablets = 63;
        private const double MaxSalt = 3;
        private const double MaxRinseAid = 1;

        public DishWasher()
        {
            Tablets = MaxTablets;
            Salt = MaxSalt;
            RinseAid = MaxRinseAid;
        }



        public void Start(WashingProgram program)
        {
            if (Tablets == 0)
            {
                RefillTablets();
            }
            if (Salt <GetConsumption(program, 60))
            {
                RefillSalt();
            }
            if (RinseAid < GetConsumption(program, 40))
            {
                RefillRinseAid();
            }
            Tablets -= 1;
            SubtractSalt(program);
            SubtractRinseAid(program);
            ProgramInstances.Add(new WashingProgramInstance { Program = program, StartTime = DateTime.Now });
            //update last clean program if the program is a clean program
            if (program.Name == "Clean Cycle")
            {
                LastCleanProgram = DateTime.Now;
            }
        }
        public void Stop()
        {

            ProgramInstances.Last().IsStopped = true;

        }
        public double GetConsumption(WashingProgram program, int capacity)
        {
            return (double)program.WaterConsumption / 60;
        }

        public Statistics GetStatistics(List<WashingProgramInstance> history)
        {
            return new Statistics(history);
        }

        public MachineStatus GetCurrentProgram()
        {
            if (ProgramInstances.Any())
            {
                var lastProgram = ProgramInstances.Last();
                if (lastProgram.StartTime.AddMinutes(lastProgram.Program.Duration) > DateTime.Now && !lastProgram.IsStopped)
                {
                    return new MachineStatus { CurrentProgram = lastProgram, RefillWarnings = GetRefillWarnings() };
                }
            }
            return new MachineStatus { CurrentProgram = null, RefillWarnings = new List<RefillWarning>() };
        }

        public List<RefillWarning> GetRefillWarnings()
        {
            List<RefillWarning> warnings = new List<RefillWarning>();
            if(Salt / MaxSalt < 0.1)
            {
                warnings.Add(new RefillWarning("Salt is running low"));
            }
            if (RinseAid / MaxRinseAid < 0.1)
            {
                warnings.Add(new RefillWarning("Rinse Aid is running low"));
            }
            if (Tablets / MaxTablets < 0.1)
            {
                warnings.Add(new RefillWarning("Tablets are running low"));
            }
            if(ShouldClean())
            {
                warnings.Add(new RefillWarning("Clean the dishwasher"));
            }
            return warnings;
        }
        private void SubtractSalt(WashingProgram program)
        {
            Salt -= GetConsumption(program, 60);
        }
        private void SubtractRinseAid(WashingProgram program)
        {
            RinseAid -= GetConsumption(program, 40);
        }

        private void RefillTablets()
        {
            Tablets = MaxTablets;
        }
        private void RefillSalt()
        {
            Salt = MaxSalt;
        }
        private void RefillRinseAid()
        {
            RinseAid = MaxRinseAid;
        }

        private bool ShouldClean()
        {
            List<WashingProgramInstance> programsSinceLastClean = ProgramInstances.Where(p => p.StartTime > LastCleanProgram).ToList();
            return programsSinceLastClean.Sum(p => p.Program.Duration) > 50 * 60;
        }
    }
}
