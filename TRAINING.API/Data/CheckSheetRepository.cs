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

        public async Task<IEnumerable<EHAL>> GetListEmployee(InventoryParams prm)
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
            if (!string.IsNullOrEmpty(prm.filled))
            {
                query = query.Where(x => x.ELTRDT.HasValue == true);
            }
            if (!string.IsNullOrEmpty(prm.unfilled))
            {
                query = query.Where(x => x.ELTRDT.HasValue == false);
            }
            if (!string.IsNullOrEmpty(prm.mustcheck))
            {
                query = query.Where(x => x.ELRCST == 1);
            }
            if (!string.IsNullOrEmpty(prm.noneedcheck))
            {
                query = query.Where(x => x.ELRCST == 0);
            }
            if (!string.IsNullOrEmpty(prm.alreadycheck))
            {
                query = query.Where(x => x.ELHCDT.HasValue);
            }
            if (!string.IsNullOrEmpty(prm.notyetcheck))
            {
                query = query.Where(x => x.ELHCDT.HasValue == false);
            }

            return await query.ToListAsync();
        }

        public async Task<PagedList<EHAL>> GetListEmployeePaging(InventoryParams prm)
        {
            var query = _context.EHAL.AsQueryable();

            if (prm.attendance == 1)
            {
                query = query.OrderByDescending(x => x.ELATDT);
            }
            else 
            {
                query = query.OrderBy(x => x.ELEMNA);
            }

            if (!string.IsNullOrEmpty(prm.nik))
            {
                query = query.Where(x => x.ELEMNO == prm.nik);
            }
            if (!string.IsNullOrEmpty(prm.name))
            {
                query = query.Where(x => x.ELEMNA.Contains(prm.name));
            }
            if (!string.IsNullOrEmpty(prm.filter))
            {
                query = query.Where(x => x.ELEMNA.Contains(prm.filter) || x.ELEMNO.Contains(prm.filter));
            }
            if (!string.IsNullOrEmpty(prm.dept))
            {
                query = query.Where(x => x.ELDENO == prm.dept);
            }
            if (!string.IsNullOrEmpty(prm.filled))
            {
                query = query.Where(x => x.ELTRDT.HasValue == true);
            }
            if (!string.IsNullOrEmpty(prm.unfilled))
            {
                query = query.Where(x => x.ELTRDT.HasValue == false);
            }
            if (!string.IsNullOrEmpty(prm.mustcheck))
            {
                query = query.Where(x => x.ELRCST == 1);
            }
            if (!string.IsNullOrEmpty(prm.noneedcheck))
            {
                query = query.Where(x => x.ELRCST == 0);
            }
            if (!string.IsNullOrEmpty(prm.alreadycheck))
            {
                query = query.Where(x => x.ELHCDT.HasValue);
            }
            if (!string.IsNullOrEmpty(prm.notyetcheck))
            {
                query = query.Where(x => x.ELHCDT.HasValue == false);
            }

            return await PagedList<EHAL>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }

        public async Task<IEnumerable<LebaranSummaryDto>> GetSummaryLebaran(string location)
        {
            var query = _context.EHAL.Where(x => x.ELBRNO == location)
                        .GroupBy(x => new { x.ELDENO, x.ELDENA }).OrderBy(x => x.Key.ELDENA)
                        .Select(e => new LebaranSummaryDto
                        {
                            DepartmentId = e.Key.ELDENO,
                            Department = e.Key.ELDENA,
                            Filled = e.Sum(e => e.ELTRDT.HasValue ? 1 : 0),
                            Unfilled = e.Sum(e => e.ELTRDT.HasValue == false ? 1 : 0),
                            MustCheck = e.Sum(e => e.ELRCST == 1 ? 1 : 0),
                            NoNeedCheck = e.Sum(e => e.ELRCST == 0 ? 1 : 0),
                            AlreadyCheck = e.Sum(e => (e.ELRCST == 1 && e.ELHCDT.HasValue) ? 1 : 0),
                            NotYetCheck = e.Sum(e => e.ELRCST == 1 && e.ELHCDT.HasValue == false ? 1 : 0)
                        });
            
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<DropdownDto>> GetDepartment(string location)
        {
            var query = from e in _context.EHAL.Where(x => x.ELBRNO == location)
                        .Select(x => new { x.ELDENO, x.ELDENA }).Distinct()
                        select new DropdownDto 
                        {
                            Label = e.ELDENA,
                            Value = e.ELDENO
                        };
            
            return await query.ToListAsync();
        }

        public async Task<ELOG> GetTapLog(int id)
        {
            return await _context.ELOG.FirstOrDefaultAsync(x => x.ELTRID == id);
        }

        public async Task<PagedList<ELOG>> GetListTapLog(InventoryParams prm)
        {
            var query = _context.ELOG.Where(x => x.ELRCID == prm.id)
                        .OrderByDescending(x => x.ELTRDT)
                        .AsQueryable();
            
            if (!string.IsNullOrEmpty(prm.name))
            {
                query = query.Where(x => x.ELEMNA.Contains(prm.name));
            }

            return await PagedList<ELOG>.CreateAsync(query, prm.PageNumber, prm.PageSize);           
        }

        public async Task<ELOH> GetTapHeader(int id)
        {
            return await _context.ELOH.FirstOrDefaultAsync(x => x.EHRCID == id);
        }

        public async Task<IEnumerable<ELOH>> GetListTapHeader()
        {
            return await _context.ELOH.Where(x => x.EHRCST == 1).ToListAsync();
        }

        public async Task<int> GetTapLogCount(DateTime dt)
        {
            return await _context.ELOG.Where(x => 
                x.ELTRDT.Value.Year == dt.Year &&
                x.ELTRDT.Value.Month == dt.Month &&
                x.ELTRDT.Value.Day == dt.Day).CountAsync();
        }

        
    }
}