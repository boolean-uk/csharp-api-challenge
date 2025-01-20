namespace Dishwasher.engine;

public class DishwasherProgramsRepository
{
    private readonly DishwasherProgramsData _programsData;

    public DishwasherProgramsRepository(DishwasherProgramsData programsData)
    {
        _programsData = programsData;
    }

    #region Core

    public List<DishwasherProgram> GetAll()
    {
        return _programsData.GetAll();
    }

    public DishwasherRunningProgram Start(DishwasherProgram dishwasherProgram)
    {
        return _programsData.Start(dishwasherProgram);
    }

    public DishwasherRunningProgram GetRunning()
    {
        return _programsData.GetRunning();
    }

    public List<DishwasherRunningProgram> GetProgramsHistory()
    {
        return _programsData.GetProgramsHistory();
    }

    public Statistics GetStatistics()
    {
        return _programsData.GetStatistics();
    }

    #endregion

    #region Extensions

    public DishwasherRunningProgram Stop()
    {
       return _programsData.Stop();
    }

    public Decimal GetRinseAid()
    {
        return _programsData.GetRinseAid();
    }

    public void UpdateRinseAid(decimal update)
    {
        _programsData.UpdateRinseAid(update);
    }

    public Decimal GetSalt()
    {
        return _programsData.GetSalt();
    }

    public void UpdateSalt(decimal update)
    {
        _programsData.UpdateSalt(update);
    }

    public int GetTablets()
    {
        return _programsData.GetTablets();
    }

    public void UpdateTablets(int update)
    {
        _programsData.UpdateTablets(update);
    }

    public Decimal GetCleanCycle()
    {
        return _programsData.GetCleanCycle();
    }

    public void UpdateCleanCycle(decimal update)
    {
        _programsData.UpdateCleanCycle(update);
    }

    #endregion

    public DishwasherProgram Add(DishwasherProgram dishwasherProgram)
    {
        return _programsData.Add(dishwasherProgram);
    }

    public DishwasherProgram Get(int id)
    {
        return _programsData.Get(id);
    }


}
