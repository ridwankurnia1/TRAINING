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
    public class ProductionRepository2 : IProductionRepository2
    {
        private readonly AMGContext _context;

        public ProductionRepository2(AMGContext context)
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

        public async Task<IEnumerable<MDF1>> GetMDF1()
        {
            return await _context.MDF1.ToListAsync();
        }

        public async Task<MDF1> GetMDF1ById(string id) //get list MDF0 berdasarkan DDTRID
        {                      
            return await _context.MDF1.FirstOrDefaultAsync(x => x.DEDFNO == id);
        }

        public async Task<PagedList<MDF1>> GetListDefect2Paging(Params prm)
        {
            var query = _context.MDF1.OrderBy(x => x.DEDFNO).AsQueryable();
            
            if(!string.IsNullOrEmpty(prm.filter))
            {
                query = query.Where(x => x.DEDFNO.Contains(prm.filter) ||
                x.DEDFNA.Contains(prm.filter) ||
                x.DEDFG1.Contains(prm.filter));
                // if(prm.filter == "1"){
                //     query = query.Where(x => x.DERCST == 1);
                //     }
            }
            if (prm.status == "1")
            {
                query = query.Where(x => x.DERCST == 1);
            }
            
            if (prm.status == "0")
            {
                query = query.Where(x => x.DERCST == 0);
            }

            return await PagedList<MDF1>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }
        public async Task<IEnumerable<MDF0>> GetDefectGroup()
        {
            return await _context.MDF0.Where(x => x.DDRCST == 1).ToListAsync();
        }
        
    }
}