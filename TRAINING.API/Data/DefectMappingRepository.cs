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
    public class DefectMappingRepository : IDefectMappingRepository
    {
        private readonly AMGContext _context;

        private readonly string Company = "amg";
        private readonly string Branch = "ckp";
        private readonly string zvar = "dfty";
        private readonly string CbtNo = "LPG2";

        public DefectMappingRepository(AMGContext context)
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

        public async Task<PagedList<MDMP>> GetMdmpPaging(ProductionParams prm)
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

        public async Task<IEnumerable<GCT2>> GetLineProcessGroup() //get list MDF0 berdasarkan DDTRID
        {                      
            return await _context.GCT2.Where(x => x.CBCONO == Company && x.CBBRNO == Branch && x.CBTBNO == CbtNo && x.CBRCST == 1).ToListAsync();

            // return await _context.GCT2.FirstOrDefault(x => x.CBCONO == Company && x.CBBRNO == Branch && x.CBRCST == 1);
        }

        public async Task<IEnumerable<ZVAR>> GetZvarDefectType() //get list MDF0 berdasarkan DDTRID
        {                      
            return await _context.ZVAR.Where(x => x.ZRCONO == Company && x.ZRVATY == zvar ).ToListAsync();
        }
    }
}