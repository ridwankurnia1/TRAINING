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
            .Select(col => new IPTY
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
            })
            .AsQueryable();
            if (!string.IsNullOrEmpty(param.SearchString))
            {
                query.Where(c => 
                c.HSPETY.Contains(param.SearchString) ||
                c.HSPLAP.Contains(param.SearchString) ||
                c.HSMATY.Contains(param.SearchString) ||
                c.HSCLNO.Contains(param.SearchString));
            }
            
            return await PagedList<IPTY>.CreateAsync(query, param.PageNumber, param.PageSize);
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
    }
}