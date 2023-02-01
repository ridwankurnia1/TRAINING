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
    public class ProductionRepository3 : IProductionRepository3
    {
        private readonly AMGContext _context;

        public ProductionRepository3(AMGContext context)
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

        public async Task<IEnumerable<MDMP>> GetMDMP()
        {
            return await _context.MDMP.ToListAsync();
        }
        
        public async Task<MDMP> GetMdmpById(string id) //get list MDMP berdasarkan DDTRID
        {                      
            return await _context.MDMP.FirstOrDefaultAsync(x => x.DMDFNO == id);
        }

        public async Task<PagedList<MDMP>> GetMdmpPaging(Params prm)
        {
            var query = _context.MDMP.OrderBy(x => x.DMDFNO).AsQueryable();
            
            if(!string.IsNullOrEmpty(prm.filter))
            {
                query = query.Where(x => x.DMDFNO.Contains(prm.filter) ||
                x.DMDFTY.Contains(prm.filter) ||
                x.DMLPGR.Contains(prm.filter));
                // if(prm.filter == "1"){
                //     query = query.Where(x => x.DERCST == 1);
                //     }
            }
            if (prm.status == "1")
            {
                query = query.Where(x => x.DMRCST == 1);
            }
            
            if (prm.status == "0")
            {
                query = query.Where(x => x.DMRCST == 0);
            }

            return await PagedList<MDMP>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }
        public async Task<MDF1> GetMDF1ByDefectType(string defectType) //get list MDF0 berdasarkan DDTRID
        {                      
            return await _context.MDF1.FirstOrDefaultAsync(x => x.DEDPGR == defectType);
        }
    }
}