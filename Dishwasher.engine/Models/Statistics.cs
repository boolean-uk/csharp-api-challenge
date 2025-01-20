
namespace Dishwasher.engine;

public class Statistics
{
    public decimal TotalWater { get; set; }
    public decimal TotalElectricity { get; set; }
    public decimal AverageWater { get; set; }
    public decimal AverageElectricity { get; set; }

    public Statistics(List<DishwasherRunningProgram> programsHistory)
    {
        TotalWater = programsHistory.Sum(p => p.WaterConsumption);
        TotalElectricity = programsHistory.Sum(p => p.ElectricConsumption);

        int count = programsHistory.Count;
        AverageWater = count > 0 ? TotalWater / count : 0;
        AverageElectricity = count > 0 ? TotalElectricity / count : 0;
    }
}
