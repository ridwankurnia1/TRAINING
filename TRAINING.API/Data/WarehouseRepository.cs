using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AMGContext _context;

        public WarehouseRepository(AMGContext context)
        {
            _context = context;
        }

        public IQueryable<IWGRX> Query()
        {
            return _context.IWGRX.AsQueryable();
        }

        public async Task<IList<IWGRX>> AllGroup()
        {
            return await _context.IWGRX.AsNoTracking().ToListAsync();
        }

        public async Task<IWGRX> SingleGroup(string code)
        {
            return await _context.IWGRX.AsNoTracking().FirstOrDefaultAsync(c => c.HVWHGR == code);
        }

        public async Task<bool> CreateGroup(IWGRX data)
        {
            _context.Add(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateGroup(IWGRX data)
        {
            _context.Update(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGroup(IWGRX data)
        {
            _context.Remove(data);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}