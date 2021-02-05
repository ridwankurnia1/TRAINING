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
        private readonly APRISEContext _apriseContext;

        public UserRepository(AMGContext context, APRISEContext apriseContext)
        {
            _apriseContext = apriseContext;
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
            return await _context.MEMP.FirstOrDefaultAsync(x => x.EMEMNO == nik);
        }

        public async Task<PagedList<MEMP>> GetListEmmployee(Params prm)
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

            return await PagedList<MEMP>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }

        public async Task<IEnumerable<GOG1>> GetOrganization()
        {
            return await _context.GOG1.Where(x => x.GORCST == 1).ToListAsync();
        }

        public async Task<List<EMPLOYEE>> GetListEmmployeeAPRISE(Params prm)
        {
            var query = _apriseContext.EMPLOYEE.AsQueryable();

            if (!string.IsNullOrEmpty(prm.name))
            {
                query = query.Where(x => x.NAME.Contains(prm.name));
            }
            if (!string.IsNullOrEmpty(prm.dept))
            {
                query = query.Where(x => x.ORGANIZATIONSTRUCTURE.Contains(prm.dept));
            }
            if (!string.IsNullOrEmpty(prm.grade))
            {
                query = query.Where(x => x.GRADECODE == prm.grade);
            }

            return await query.ToListAsync();
        }
    }
}