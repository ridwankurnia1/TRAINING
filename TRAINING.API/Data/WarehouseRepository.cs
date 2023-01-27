using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Helper;
using TRAINING.API.Model;
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

        public IQueryable<IWHSX> Query()
        {
            return _context.IWHSX.AsQueryable();
        }

        public async Task<PagedList<IWHSX>> All(WarehouseParams warehouseParams)
        {
            var query = Query().AsNoTracking();
            return await PagedList<IWHSX>.CreateAsync(query, warehouseParams.PageNumber, warehouseParams.PageSize);
        }

        public async Task<IWHSX> Single(string code)
        {
            return await Query().AsNoTracking().FirstOrDefaultAsync(c => c.HWWHNO == code);
        }

        public async Task<bool> Create(IWHSX data)
        {
            _context.Add(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(IWHSX data)
        {
            _context.Update(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(string code)
        {
            var data = await Single(code);

            if (data == null)
            {
                return false;
            }
            
            _context.Remove(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<IWGRX> QueryGroup()
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

        public async Task<IList<GCT2>> AllType()
        {
            return await _context.GCT2.Where(c=> c.CBTBNO == "WHTY" && c.CBRCST == 1).ToListAsync();
        }
    }
}