
namespace Dishwasher.engine;

public class DishwasherProgram
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal WaterConsumption { get; set; } // Liters
    public decimal ElectricConsumption { get; set; } // kWh per cycle
    public int Runtime {  get; set; } // Seconds

    public DishwasherProgram(int id, string name, decimal waterConsumption, decimal electricConsumption, int runtime)
    {
        Id = id;
        Name = name;
        WaterConsumption = waterConsumption;
        ElectricConsumption = electricConsumption;
        Runtime = runtime;
    }
}
