

namespace Dishwasher.engine;

public class DishwasherProgramsData
{
    List<DishwasherProgram> Programs = new List<DishwasherProgram>()
    {
        new DishwasherProgram(1, "Intensive 70", 13.5m, 1.35m, 9000),
        new DishwasherProgram(2, "Eco 50", 9m, 0.65m, 3600),
        new DishwasherProgram(3, "Half Load", 10.5m, 1.1m, 2400),
        new DishwasherProgram(4, "Clean Cycle", 14m, 1.45m, 3300)
    };

    DishwasherRunningProgram RunningProgram;

    private Queue<DishwasherRunningProgram> ProgramsHistory = new Queue<DishwasherRunningProgram>();

    public decimal RinseAid = 40m;
    public decimal Salt = 60m;
    public int Tablets = 63;
    public decimal CleanCycle = 50m;

    #region Core
    public List<DishwasherProgram> GetAll()
    {
        return Programs;
    }

    public DishwasherRunningProgram Start(DishwasherProgram dishwasherProgram)
    {
        if (RunningProgram == null)
        {
            RunningProgram = new DishwasherRunningProgram(dishwasherProgram);

            RinseAid = RinseAid - RunningProgram.WaterConsumption;

            if (ProgramsHistory.Count > 150)
            {
                ProgramsHistory.Dequeue();
            }
            return RunningProgram;
        }
        else
        {
            return RunningProgram;
        }
    }

    public DishwasherRunningProgram GetRunning()
    {
        return RunningProgram;
    }

    public List<DishwasherRunningProgram> GetProgramsHistory()
    {
        return ProgramsHistory.ToList();
    }

    public DishwasherProgram Add(DishwasherProgram dishwasherProgram)
    {
        Programs.Add(dishwasherProgram);
        return dishwasherProgram;
    }

    public Statistics GetStatistics()
    {
        return new Statistics(ProgramsHistory.ToList());
    }
    #endregion

    #region Extensions

    public DishwasherRunningProgram Stop()
    {
        DishwasherRunningProgram dishwasherRunningProgram = RunningProgram;
        RunningProgram = null;
        return dishwasherRunningProgram;
    }

    public Decimal GetRinseAid()
    {
        return RinseAid;
    }

    public void UpdateRinseAid(decimal update)
    {
        RinseAid = RinseAid + update;
    }

    public Decimal GetSalt()
    {
        return Salt;
    }

    public void UpdateSalt(decimal update)
    {
        Salt = Salt + update;
    }

    public int GetTablets()
    {
        return Tablets;
    }

    public void UpdateTablets(int update)
    {
        Tablets = Tablets + update;
    }

    public decimal GetCleanCycle()
    {
        return CleanCycle;
    }

    public void UpdateCleanCycle(decimal update)
    {
        CleanCycle = CleanCycle + update;
    }

    #endregion


    public DishwasherProgram Get(int id)
    {
        return Programs.Where(p => p.Id == id).FirstOrDefault();
    }
}
