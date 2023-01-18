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
        
        public async Task<MDF0> GetMDF0ByDdtrid (int ddtrid)
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDTRID == ddtrid);
        }

        public async Task<IEnumerable<MDF0>> GetMDF0ByDddfgr (string dddfgr)
        {
            return await _context.MDF0.Where(x => x.DDDFGR == dddfgr).ToListAsync();
        }

        public async Task<IEnumerable<MDF0>> GetMdf0By (string DefectGroup, int transactionId )
        {
            return await _context.MDF0.Where(x => x.DDDFGR == DefectGroup & x.DDTRID == transactionId).ToListAsync();
        }


        public async Task<MDF0> GetDfGMDF0 (string dddfgr)
        {
            return await _context.MDF0.FirstOrDefaultAsync(x => x.DDDFGR == dddfgr);
        }

        public async Task<PagedList<MDF0>> GetListDefectPaging(Params prm)
        {
            var query = _context.MDF0.OrderBy(x => x.DDTRID).AsQueryable();

            if(!string.IsNullOrEmpty(prm.filter))
            {
                query = query.Where(x => x.DDDFGR.Contains(prm.filter));
            }

            return await PagedList<MDF0>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }
    }
}