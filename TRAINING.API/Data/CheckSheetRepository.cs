using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public class CheckSheetRepository : ICheckSheetRepository
    {
        private readonly ECSContext _context;
        public CheckSheetRepository(ECSContext context)
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

        public async Task<EHAL> GetEmployee(string nik, string rfid)
        {
            if (!string.IsNullOrEmpty(nik))
                return await _context.EHAL.FirstOrDefaultAsync(x => x.ELEMNO == nik);
            
            return await _context.EHAL.FirstOrDefaultAsync(x => x.ELRFID == rfid);
        }

        public async Task<IEnumerable<EHAL>> GetListEmployee(Params prm)
        {
            var query = _context.EHAL.AsQueryable();

            if (!string.IsNullOrEmpty(prm.name))
            {
                query = query.Where(x => x.ELEMNA.Contains(prm.name));
            }
            if (!string.IsNullOrEmpty(prm.dept))
            {
                query = query.Where(x => x.ELDENA.Contains(prm.dept));
            }
            if (!string.IsNullOrEmpty(prm.Filled))
            {
                query = query.Where(x => x.ELTRDT.HasValue == true);
            }
            if (!string.IsNullOrEmpty(prm.Unfilled))
            {
                query = query.Where(x => x.ELTRDT.HasValue == false);
            }
            if (!string.IsNullOrEmpty(prm.MustCheck))
            {
                query = query.Where(x => x.ELRCST == 1);
            }
            if (!string.IsNullOrEmpty(prm.NoNeedCheck))
            {
                query = query.Where(x => x.ELRCST == 0);
            }
            if (!string.IsNullOrEmpty(prm.AlreadyCheck))
            {
                query = query.Where(x => x.ELHCDT.HasValue);
            }
            if (!string.IsNullOrEmpty(prm.NotYetCheck))
            {
                query = query.Where(x => x.ELHCDT.HasValue == false);
            }

            return await query.ToListAsync();
        }

        public async Task<PagedList<EHAL>> GetListEmployeePaging(Params prm)
        {
            var query = _context.EHAL.OrderBy(x => x.ELEMNA).AsQueryable();

            if (!string.IsNullOrEmpty(prm.nik))
            {
                query = query.Where(x => x.ELEMNO == prm.nik);
            }
            if (!string.IsNullOrEmpty(prm.name))
            {
                query = query.Where(x => x.ELEMNA.Contains(prm.name));
            }
            if (!string.IsNullOrEmpty(prm.dept))
            {
                query = query.Where(x => x.ELDENA.Contains(prm.dept));
            }
            if (!string.IsNullOrEmpty(prm.Filled))
            {
                query = query.Where(x => x.ELTRDT.HasValue == true);
            }
            if (!string.IsNullOrEmpty(prm.Unfilled))
            {
                query = query.Where(x => x.ELTRDT.HasValue == false);
            }
            if (!string.IsNullOrEmpty(prm.MustCheck))
            {
                query = query.Where(x => x.ELRCST == 1);
            }
            if (!string.IsNullOrEmpty(prm.NoNeedCheck))
            {
                query = query.Where(x => x.ELRCST == 0);
            }
            if (!string.IsNullOrEmpty(prm.AlreadyCheck))
            {
                query = query.Where(x => x.ELHCDT.HasValue);
            }
            if (!string.IsNullOrEmpty(prm.NotYetCheck))
            {
                query = query.Where(x => x.ELHCDT.HasValue == false);
            }

            return await PagedList<EHAL>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }

        public async Task<IEnumerable<LebaranSummaryDto>> GetSummaryLebaran(string location)
        {
            var query = _context.EHAL.Where(x => x.ELBRNO == location)
                        .GroupBy(x => x.ELDENA)
                        .Select(e => new LebaranSummaryDto
                        {
                            Department = e.Key,
                            Filled = e.Sum(e => e.ELTRDT.HasValue ? 1 : 0),
                            Unfilled = e.Sum(e => e.ELTRDT.HasValue == false ? 1 : 0),
                            MustCheck = e.Sum(e => e.ELRCST == 1 ? 1 : 0),
                            NoNeedCheck = e.Sum(e => e.ELRCST == 0 ? 1 : 0),
                            AlreadyCheck = e.Sum(e => e.ELRCST == 1 && e.ELHCDT.HasValue ? 1 : 0),
                            NotYetCheck = e.Sum(e => e.ELRCST == 1 && e.ELHCDT.HasValue == false ? 1 : 0)
                        });
            
            return await query.ToListAsync();
        }        
    }
}