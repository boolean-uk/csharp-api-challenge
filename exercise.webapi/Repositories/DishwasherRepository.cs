using exercise.engine.Models;
using exercise.webapi.Data;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repositories
{
    public class DishwasherRepository : IRepository
    {
        private static readonly DishWasher BaseDishWasher = new();
        private DataContext _db;
        private DishWasher _dishwasher;
        public DishwasherRepository(DataContext db, DishWasher dishwasher = null)
        {
            _db = db;
            _dishwasher = dishwasher ?? BaseDishWasher;
        }

        public async Task<IEnumerable<WashingProgram>> GetAllAsync()
        {
            return await _db.Programs.ToListAsync();
        }

        public async Task<WashingProgram> GetByIdAsync(int id)
        {
            return await _db.Programs.FindAsync(id);
        }

        public MachineStatus GetCurrentProgram()
        {
            return _dishwasher.GetCurrentProgram();
        }

        public IEnumerable<WashingProgramInstance> GetHistory()
        {
            return  _dishwasher.ProgramInstances.OrderByDescending(p => p.StartTime).Take(150).ToList();
        }

        public async Task<Statistics> GetStatistics()
        {
            List<WashingProgramInstance> history = (List<WashingProgramInstance>) GetHistory();
            return _dishwasher.GetStatistics(history);
        }

        public async Task<WashingProgram> StartProgram(int id)
        {
            var program = await _db.Programs.FindAsync(id);
            _dishwasher.Start(program);
            return program;
        }
        public async Task StopProgram()
        {
            _dishwasher.Stop();
        }
    }
}
