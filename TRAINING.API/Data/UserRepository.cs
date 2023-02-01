using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TRAINING.API.Model;
using TRAINING.API.Helper;
using System.Linq;
using System.Collections.Generic;

namespace TRAINING.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AMGContext _context;
        // private readonly APRISEContext _apriseContext;

        public UserRepository(AMGContext context)
        {
            _context = context;            
        }

        public void Add<T> (T entity) where T : class 
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

        public async Task<MEMP> GetEmployee(string nik)
        {
            return await _context.MEMP.FirstOrDefaultAsync(x => x.EMEMNO == nik && x.EMRCST == 1);
        }
        public async Task<MEMP> GetEmployee(string nik, string rfid)
        {
            if (!string.IsNullOrEmpty(nik))
                return await _context.MEMP.Include(x => x.GOG1).FirstOrDefaultAsync(x => x.EMEMNO == nik);
            
            return await _context.MEMP.Include(x => x.GOG1).FirstOrDefaultAsync(x => x.EMRFID == rfid);
        }

        public async Task<PagedList<MEMP>> GetListEmmployee(InventoryParams prm)
        {
            var query = _context.MEMP.Include(x => x.GOG1).Where(x => x.EMRCST == 1).AsQueryable();

            if (!string.IsNullOrEmpty(prm.name))
            {
                query = query.Where(x => x.EMEMNA.Contains(prm.name));
            }
            if (!string.IsNullOrEmpty(prm.dept))
            {
                query = query.Where(x => x.EMDENO.Contains(prm.dept));
            }
            if (!string.IsNullOrEmpty(prm.grade))
            {
                query = query.Where(x => x.EMEGNO == prm.grade);
            }
            if (!string.IsNullOrEmpty(prm.filter))
            {
                query = query.Where(x => x.EMEMNO.Contains(prm.filter) || 
                        x.EMEMNA.Contains(prm.filter) ||
                        x.GOG1.GOOGNA.Contains(prm.filter));
            }

            return await PagedList<MEMP>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }

        public async Task<IEnumerable<GOG1>> GetOrganization()
        {
            return await _context.GOG1.Where(x => x.GORCST == 1).ToListAsync();
        }

        // public async Task<List<EMPLOYEE>> GetListEmmployeeAPRISE(Params prm)
        // {
        //     var query = _apriseContext.EMPLOYEE.AsQueryable();

        //     if (!string.IsNullOrEmpty(prm.name))
        //     {
        //         query = query.Where(x => x.NAME.Contains(prm.name));
        //     }
        //     if (!string.IsNullOrEmpty(prm.dept))
        //     {
        //         query = query.Where(x => x.ORGANIZATIONSTRUCTURE.Contains(prm.dept));
        //     }
        //     if (!string.IsNullOrEmpty(prm.grade))
        //     {
        //         query = query.Where(x => x.GRADECODE == prm.grade);
        //     }

        //     return await query.ToListAsync();
        // }

        public async Task<IEnumerable<MGRD>> GetListGrade()
        {
            return await _context.MGRD.ToListAsync();
        }
    }
}