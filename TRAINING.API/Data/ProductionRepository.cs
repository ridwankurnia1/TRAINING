using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;
using System;

namespace TRAINING.API.Data
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly AMGContext _context;

        public ProductionRepository(AMGContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MDF0>> GetListMDF0()
        {
            return await _context.MDF0.ToListAsync();
        }

        public async Task<MDF0> Get1MDF0(int id) //get list MDF0 berdasarkan DDTRID
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDTRID == id);
        }

        public async Task<MDF0> GetMDF0ByParams(int id) //get list MDF0 berdasarkan DDTRID
        {
            // return await _context.MDF0.FirstOrDefaultAsync(x => x.DDTRID == prm.id);
            // return await _context.MDF0.Where(x => x.DDTRID == prm.id);
            // var query = _context.MDF0.Where(x => x.DDTRID == prm.id);
            // return await _context.MDF0.Where(x => x.DDTRID == prm.id);

            // return await PagedList<MDF0>.Create(query); 
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDTRID == id);

            // var query = _context.MDF0.Where(x => x.DDTRID == prm.id)
            //             .AsQueryable();
            // return await PagedList<MDF0>.CreateAsync(query); 
        }
        public async Task<MDF0> GetMDF0ByParamsName(string name) //get list MDF0 berdasarkan DDTRID
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDCRUS == name);
        }

        public async Task<MDF0> GetNameMDF0(string nama) //get list MDF0 berdasarkan nama
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDCHUS == nama);
        }

        public async Task<IEnumerable<MDF0>> FindListMDF0()
        {
            // return await _context.MDF0.ToListAsync();
            // return _context.MDF0
            //                .Where(s => s.DDDFGR == "TEST" )
            //                .Include(s => s.DDCHUS == "REGANANDA")
            //                .FirstOrDefault();
            return await _context.MDF0.ToListAsync();
        }

        public async Task<MDF0> FindListMDF0(int ddtrid)
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDTRID == ddtrid);
        }
        public async Task<MDF0> DeleteListMDF0(int ddtrid)
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDTRID == ddtrid);
        }

        public async Task<MDF0> GetMDF0ByDdtrid(int ddtrid)
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDTRID == ddtrid);
        }

        public async Task<IEnumerable<MDF0>> GetMDF0ByDddfgr(string dddfgr)
        {
            return await _context.MDF0.Where(x => x.DDDFGR == dddfgr).ToListAsync();
        }

        public async Task<IEnumerable<MDF0>> GetMdf0By(string DefectGroup, int transactionId)
        {
            return await _context.MDF0.Where(x => x.DDDFGR == DefectGroup & x.DDTRID == transactionId).ToListAsync();
        }


        public async Task<MDF0> GetDfGMDF0(string dddfgr)
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDDFGR == dddfgr);
        }

        public async Task<PagedList<MDF0>> GetListDefectPaging(InventoryParams prm)
        {
            var query = _context.MDF0.OrderBy(x => x.DDTRID).AsQueryable();


            if (!string.IsNullOrEmpty(prm.filter))
            {
                // if (prm.filter == "1")
                //     query.Where(x => x.DDRCST == 1);
                // if (prm.filter == "0")
                //     query.Where(x => x.DDRCST == 0);

                query = query.Where(x => x.DDDFGR.Contains(prm.filter) ||
                x.DDREMA.Contains(prm.filter)
                );

            }

            if (prm.status == "1")
            {
                query = query.Where(x => x.DDRCST == 1);
            }
            if (prm.status == "0")
            {
                query = query.Where(x => x.DDRCST == 0);
            }

            // if(!string.IsNullOrEmpty(prm.remark))
            // {
            //     query = query.Where(x => x.DDREMA.Contains(prm.remark));
            // }

            // if((prm.status).Equals(null))
            // {
            //     query = query.Where(x => x.DDRCST == 1);
            // }
            // else
            // {
            //     query = query.Where(x => x.DDRCST == 0);
            // }

            // if (!string.IsNullOrEmpty(prm.mustcheck))
            // {
            //     query = query.Where(x => x.DDRCST == 1);
            // }

            // if (!string.IsNullOrEmpty(prm.noneedcheck))
            // {
            //     query = query.Where(x => x.DDRCST == 0);
            // }



            return await PagedList<MDF0>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }

        public async Task<PagedList<TRCK>> GetListTruck(TruckParams param)
        {
            var query = _context.TRCK.AsQueryable();

            //Sort di tombol search
            if (!string.IsNullOrEmpty(param.filter))
            {
                query = query.Where(x =>
                x.TBTRNA.Contains(param.filter)
                || x.TBDRNA.Contains(param.filter)
                || x.TBMRKA.Contains(param.filter)
                );
            }


            //Sorting Column
            if (param.srt != null)
            {
                switch (param.srt)
                {
                    case "trna":
                        query = query.OrderBy(c => c.TBTRNA);
                        break;
                    case "-trna":
                        query = query.OrderByDescending(c => c.TBTRNA);
                        break;
                    case "mrka":
                        query = query.OrderBy(c => c.TBMRKA);
                        break;
                    case "-mrka":
                        query = query.OrderByDescending(c => c.TBMRKA);
                        break;
                    case "drna":
                        query = query.OrderBy(c => c.TBDRNA);
                        break;
                    case "-drna":
                        query = query.OrderByDescending(c => c.TBDRNA);
                        break;
                    case "jndt":
                        query = query.OrderBy(c => c.TBJNDT);
                        break;
                    case "-jndt":
                        query = query.OrderByDescending(c => c.TBJNDT);
                        break;
                    case "endt":
                        query = query.OrderBy(c => c.TBENDT);
                        break;
                    case "-endt":
                        query = query.OrderByDescending(c => c.TBENDT);
                        break;
                    case "chus":
                        query = query.OrderBy(c => c.TBCHUS);
                        break;
                    case "-chus":
                        query = query.OrderByDescending(c => c.TBCHUS);
                        break;
                    case "rcst":
                        query = query.OrderBy(c => c.TBRCST);
                        break;
                    case "-rcst":
                        query = query.OrderByDescending(c => c.TBRCST);
                        break;
                }
            }


            return await PagedList<TRCK>.CreateAsync(query, param.PageNumber, param.PageSize);
        }

        public async Task<TRCK> GetTruck(int truckId)
        {
            return await _context.TRCK.FirstOrDefaultAsync(x => x.TBRCID == truckId);
        }

        public async Task<IList<TRCK>> Export(TruckParams truckParams)
        {
            var query = _context.TRCK.AsQueryable();

            //Sort di tombol search
            if (!string.IsNullOrEmpty(truckParams.filter))
            {
                query = query.Where(x =>
                x.TBTRNA.Contains(truckParams.filter)
                || x.TBDRNA.Contains(truckParams.filter)
                || x.TBMRKA.Contains(truckParams.filter)
                );
            }

            query = query.AsNoTracking().OrderBy(c => c.TBRCID);
            return await query.ToListAsync();

        }
    }
}