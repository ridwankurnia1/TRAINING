using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public class PalletTypeRepository : IPalletTypeRepository
    {
        private readonly AMGContext _context;
        private string Company = "AMG";
        private string Branch = "CKP";

        public PalletTypeRepository(AMGContext context)
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

        public async Task<IPTY> FindByType(string palletType)
        {
            return await _context.IPTY.FirstOrDefaultAsync(d => d.HSCONO == "AMG" && d.HSBRNO == "CKP" && d.HSPETY == palletType);
        }

        public async Task<IPTY> FindByUser(string user)
        {
            return await _context.IPTY.FirstOrDefaultAsync(d => d.HSCRUS == user);
        }

        public async Task<PagedList<IPTY>> GetAllPalletTypes(PalletTypeParams param)
        {
            var query = _context.IPTY
            /*  .Select(col => new IPTY
             {
                 HSPETY = col.HSPETY,
                 HSPLAP = col.HSPLAP,
                 HSMATY = col.HSMATY,
                 HSCLNO = col.HSCLNO,
                 HSPELN = col.HSPELN,
                 HSPEWD = col.HSPEWD,
                 HSPEHG = col.HSPEHG,
                 HSPEWG = col.HSPEWG,
                 HSREMA = col.HSREMA,
                 HSCRDT = col.HSCRDT
             }) */
            .AsQueryable();

            // global search
            if (!string.IsNullOrEmpty(param.SearchString))
            {
                query = query.Where(c =>
                c.HSPETY.Contains(param.SearchString) ||
                c.HSPLAP.Contains(param.SearchString) ||
                c.HSMATY.Contains(param.SearchString) ||
                c.HSCLNO.Contains(param.SearchString));
            }

            // individual column search
            if (!string.IsNullOrEmpty(param.ptp))
            {
                query = query.Where(c => c.HSPETY.Contains(param.ptp));
            }

            if (!string.IsNullOrEmpty(param.atp))
            {
                query = query.Where(c => c.HSPLAP.Contains(param.atp));
            }

            if (!string.IsNullOrEmpty(param.mtp))
            {
                query = query.Where(c => c.HSMATY.Contains(param.mtp));
            }

            if (!string.IsNullOrEmpty(param.col))
            {
                query = query.Where(c => c.HSPETY.Contains(param.ptp));
            }

            if (param.plt != null && !string.IsNullOrEmpty(param.pltd))
            {
                query = query.Where(c => c.HSPELN > param.plt);
            }

            if (param.pwt != null && !string.IsNullOrEmpty(param.pwtd))
            {
                query = query.Where(c => c.HSPEWG > param.pwt);
            }

            // query = query.Where(c => c.HSCONO == Company && c.HSBRNO == Branch);

            return await PagedList<IPTY>.CreateAsync(query, param.PageNumber, param.PageSize);
        }

        public async Task<IList<GCUR>> GetCurrencyDefinition()
        {
            return await _context.GCUR.ToListAsync();
        }

        public async Task<IList<GCT2>> GetCommonnText2(string type)
        {
            var findType = "";

            if (type == "mtp")
            {
                findType = "PETA";
            }else{
                findType = "CLNO";
            }

            return await _context.GCT2
            .Where(c => c.CBTBNO == findType)
            .ToListAsync();
        }

        public async Task<IList<ZVAR>> GetPalletAppDefinition()
        {
            return await _context.ZVAR
            .Where(c => c.ZRVATY == "PLAP")
            .ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(string type, PalletTypeDto data)
        {
            var dataToUpdate = await FindByType(type);

            if (dataToUpdate == null)
            {
                return false;
            }

            var changeTracking = _context.Update(dataToUpdate);

            if (changeTracking.State != EntityState.Modified)
            {
                return false;
            }

            return await SaveAll();
        }

        public async Task<IList<IUOM>> GetMeasurements()
        {
            return await _context.IUOM.ToListAsync();
        }
    }
}