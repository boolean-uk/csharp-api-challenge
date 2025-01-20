using exercise.engine.Models;

namespace exercise.webapi.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<WashingProgram>> GetAllAsync();
        Task<WashingProgram> GetByIdAsync(int id);
        Task<WashingProgram> StartProgram(int id);
        MachineStatus GetCurrentProgram();
        IEnumerable<WashingProgramInstance> GetHistory();
        Task<Statistics> GetStatistics();
        Task StopProgram();


    }
}
