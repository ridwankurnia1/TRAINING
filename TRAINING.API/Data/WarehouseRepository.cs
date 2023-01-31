using System;
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

        public IQueryable<IWHS> Query()
        {
            return _context.IWHS.AsQueryable();
        }

        public AMGContext GetContext() => _context;

        public async Task<PagedList<IWHS>> All(WarehouseParams warehouseParams)
        {
            var query = from a in _context.IWHS
                        join b in _context.IWGR on a.HWWHGR equals b.HVWHGR
                        into ab
                        from b in ab.DefaultIfEmpty()
                        select new IWHS
                        {
                            HWWHNO = a.HWWHNO,
                            HWWHNA = a.HWWHNA,
                            HWNICK = a.HWNICK,
                            HWWHGR = b.HVGRNA,
                            HWDFWH = a.HWDFWH,
                            HWFIFO = a.HWFIFO,
                            HWFDAY = a.HWFDAY,
                            HWRCST = a.HWRCST,
                            HWCRTT = a.HWCRTT
                        };

            if (!string.IsNullOrEmpty(warehouseParams.ws))
            {
                // identify input
                // use ToUpper() to ignore case sensitive
                switch (warehouseParams.ws.ToUpper())
                {
                    // state search
                    case "ACTIVE":
                        query = query.Where(c => c.HWRCST == 1 || c.HWFIFO == 1);
                        break;
                    case "INACTIVE":
                        query = query.Where(c => c.HWRCST == 0 || c.HWFIFO == 0);
                        break;
                    default:
                        // string search
                        query = query.Where(c => c.HWWHNO.Contains(warehouseParams.ws) ||
                            c.HWWHNO.Contains(warehouseParams.ws) ||
                            c.HWWHNA.Contains(warehouseParams.ws) ||
                            c.HWNICK.Contains(warehouseParams.ws) ||
                            c.HWWHGR.Contains(warehouseParams.ws) ||
                            c.HWDFWH.Contains(warehouseParams.ws)
                         );
                        break;
                }
            }

            query = query.AsNoTracking().OrderBy(c => c.HWCRTT);

            return await PagedList<IWHS>.CreateAsync(query, warehouseParams.PageNumber, warehouseParams.PageSize);
        }

        public async Task<IWHS> Single(string code)
        {
            return await Query().AsNoTracking().FirstOrDefaultAsync(c => c.HWWHNO == code);
        }

        public async Task<bool> Create(IWHS data)
        {
            _context.Add(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(IWHS data)
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

        public IQueryable<IWGR> QueryGroup()
        {
            return _context.IWGR.AsQueryable();
        }

        public async Task<IList<IWGR>> AllGroup(WarehouseParams warehouseParams)
        {
            var query = QueryGroup();

            if (!string.IsNullOrEmpty(warehouseParams.gs))
            {
                // identify input
                // use ToUpper() to ignore case sensitive
                switch (warehouseParams.gs.ToUpper())
                {
                    // state search
                    case "ACTIVE":
                        query = query.Where(c => c.HVRCST == 1);
                        break;
                    case "INACTIVE":
                        query = query.Where(c => c.HVRCST == 0);
                        break;
                    default:
                        // string search
                        query = query.Where(c => c.HVWHGR.Contains(warehouseParams.gs) ||
                            c.HVGRNA.Contains(warehouseParams.gs) || c.HVREMA.Contains(warehouseParams.gs)
                         );
                        break;
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IWGR> SingleGroup(string code)
        {
            return await _context.IWGR.AsNoTracking().FirstOrDefaultAsync(c => c.HVWHGR == code);
        }

        public async Task<bool> CreateGroup(IWGR data)
        {
            _context.Add(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateGroup(IWGR data)
        {
            _context.Update(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGroup(IWGR data)
        {
            _context.Remove(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<GCT2>> AllType()
        {
            return await _context.GCT2.Where(c => c.CBTBNO == "WHTY" && c.CBRCST == 1).ToListAsync();
        }

        public async Task<IList<IWHS>> Export(WarehouseParams warehouseParams)
        {
            var query = from a in _context.IWHS
                        join b in _context.IWGR on a.HWWHGR equals b.HVWHGR
                        into ab
                        from b in ab.DefaultIfEmpty()
                        select new IWHS
                        {
                            HWWHNO = a.HWWHNO,
                            HWWHNA = a.HWWHNA,
                            HWNICK = a.HWNICK,
                            HWWHGR = b.HVGRNA,
                            HWDFWH = a.HWDFWH,
                            HWFIFO = a.HWFIFO,
                            HWFDAY = a.HWFDAY,
                            HWRCST = a.HWRCST,
                            HWCHTT = a.HWCHTT
                        };

            if (!string.IsNullOrEmpty(warehouseParams.ws))
            {
                // identify input
                // use ToUpper() to ignore case sensitive
                switch (warehouseParams.ws.ToUpper())
                {
                    // state search
                    case "ACTIVE":
                        query = query.Where(c => c.HWRCST == 1 || c.HWFIFO == 1);
                        break;
                    case "INACTIVE":
                        query = query.Where(c => c.HWRCST == 0 || c.HWFIFO == 0);
                        break;
                    default:
                        // string search
                        query = query.Where(c => c.HWWHNO.Contains(warehouseParams.ws) ||
                            c.HWWHNO.Contains(warehouseParams.ws) ||
                            c.HWWHNA.Contains(warehouseParams.ws) ||
                            c.HWNICK.Contains(warehouseParams.ws) ||
                            c.HWWHGR.Contains(warehouseParams.ws) ||
                            c.HWDFWH.Contains(warehouseParams.ws)
                         );
                        break;
                }
            }

            query = query.AsNoTracking().OrderBy(c => c.HWCHTT);

            return await query.ToListAsync();
        }
    }
}