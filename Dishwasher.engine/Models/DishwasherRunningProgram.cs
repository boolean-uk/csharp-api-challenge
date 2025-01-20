
namespace Dishwasher.engine;

public class DishwasherRunningProgram
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal WaterConsumption { get; set; }
    public decimal ElectricConsumption { get; set; }
    public int Runtime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan TimeRemaining => (EndTime - DateTime.Now).TotalSeconds > 0 ? EndTime - DateTime.Now : TimeSpan.Zero;

    public DishwasherRunningProgram(DishwasherProgram runningDishwasherProgram)
    {
        Id = runningDishwasherProgram.Id;
        Name = runningDishwasherProgram.Name;
        WaterConsumption = runningDishwasherProgram.WaterConsumption;
        ElectricConsumption = runningDishwasherProgram.ElectricConsumption;
        Runtime = runningDishwasherProgram.Runtime;
        StartTime = DateTime.Now;
        EndTime = DateTime.Now.AddSeconds(Runtime);
    }
}
