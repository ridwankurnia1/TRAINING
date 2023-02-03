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

        public async Task<PagedList<MDF1>> GetMdmpPaging(DefectMappingParams prm)
        {
            var query = from a in _context.MDF1
                        join b in _context.MDMP.Where(c => c.DMDFTY == prm.defT && c.DMLPGR == prm.lineP) on a.DEDFNO equals b.DMDFNO
                        into ab from b in ab.DefaultIfEmpty()
                        select new MDF1
                        {
                            DEDFNO = a.DEDFNO,
                            DEDFNA = a.DEDFNA
                        };

            return await PagedList<MDF1>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }

        // public async Task<PagedList<MDMP>> GetMdmpPaging(ProductionParams prm)
        // {
        //     var query = _context.MDMP.OrderBy(x => x.DMDFNO).AsQueryable();

        //     if(!string.IsNullOrEmpty(prm.filter))
        //     {
        //         query = query.Where(x => x.DMDFNO.Contains(prm.filter) ||
        //         x.DMDFTY.Contains(prm.filter) ||
        //         x.DMLPGR.Contains(prm.filter));
        //         // if(prm.filter == "1"){
        //         //     query = query.Where(x => x.DERCST == 1);
        //         //     }
        //     }

        //     if (!string.IsNullOrEmpty(prm.linePro))
        //     {
        //         query = query.Where(x => x.DMLPGR.Contains(prm.linePro));
        //     }

        //     if (!string.IsNullOrEmpty(prm.defT))
        //     {
        //         query = query.Where(x => x.DMDFTY.Contains(prm.defT));
        //     }

        //     return await PagedList<MDMP>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        // }

        public async Task<MDF1> GetMDF1ByDefectType(string defectType) //get list MDF0 berdasarkan DDTRID
        {
            return await _context.MDF1.FirstOrDefaultAsync(x => x.DEDPGR == defectType);
        }

        public async Task<MDF1> GetMDF1ByDefectCode(string defectCode) //get list MDF0 berdasarkan DDTRID
        {
            return await _context.MDF1.FirstOrDefaultAsync(x => x.DEDFNO == defectCode);
        }

        public async Task<IEnumerable<GCT2>> GetLineProcessGroup() //get list MDF0 berdasarkan DDTRID
        {
            return await _context.GCT2.Where(x => x.CBCONO == Company && x.CBBRNO == Branch && x.CBTBNO == CbtNo && x.CBRCST == 1).ToListAsync();
        }

        public async Task<IEnumerable<ZVAR>> GetZvarDefectType() //get list MDF0 berdasarkan DDTRID
        {
            return await _context.ZVAR.Where(x => x.ZRCONO == Company && x.ZRVATY == zvar).ToListAsync();
        }

        public async Task<IEnumerable<MDF1>> GetDefectCode()
        {
            return await _context.MDF1.Where(x => x.DERCST == 1).ToListAsync();
        }
    }
}