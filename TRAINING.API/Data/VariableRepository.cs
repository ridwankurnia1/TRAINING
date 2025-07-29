// VariableRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TRAINING.API.Data;
using TRAINING.API.Helper;
using TRAINING.API.Helpers;
using TRAINING.API.Model;

namespace TRAINING.API.Repositories
{
    public class VariableRepository : IVariableRepository
    {
        private readonly AMGContext _context;

        public VariableRepository(AMGContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ZVAR>> GetAllAsync(VariableParams variableParams)
        {
            var query = _context.ZVAR.AsQueryable();

            if (!string.IsNullOrEmpty(variableParams?.search))
            {
                query = query.Where(v =>
                    v.ZRVANA.Contains(variableParams.search) ||
                    v.ZRVANO.Contains(variableParams.search) ||
                    v.ZRCONO.Contains(variableParams.search) ||
                    v.ZRBRNO.Contains(variableParams.search));
            }

            return await query.OrderByDescending(v => v.ZRRCID).ToListAsync();
        }

        public async Task<ZVAR> GetByIdAsync(int id)
        {
            return await _context.ZVAR.FindAsync(id);
        }

        public async Task<ZVAR> CreateAsync(ZVAR variable)
        {
            _context.ZVAR.Add(variable);
            await _context.SaveChangesAsync();
            return variable;
        }

        public async Task<ZVAR> UpdateAsync(int id, ZVAR variable)
        {
            variable.ZRRCID = id;
            _context.Entry(variable).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return variable;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var variable = await _context.ZVAR.FindAsync(id);
            if (variable != null)
            {
                _context.ZVAR.Remove(variable);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ZVAR.AnyAsync(v => v.ZRRCID == id);
        }

        public async Task<PagedList<ZVAR>> GetListVariable(VariableParams prm)
        {
            var query = _context.ZVAR.OrderByDescending(x => x.ZRRCID).AsQueryable();

            if (!string.IsNullOrEmpty(prm.user))
            {
                query = query.Where(x => x.ZRCONO.Contains(prm.user));
            }
            if (!string.IsNullOrEmpty(prm.name))
            {
                query = query.Where(x => x.ZRBRNO.Contains(prm.name));
            }
            if (!string.IsNullOrEmpty(prm.code))
            {
                query = query.Where(x => x.ZRVANO.Contains(prm.code));
            }
            if (!string.IsNullOrEmpty(prm.value))
            {
                query = query.Where(x => x.ZRVANA.Contains(prm.value));
            }
            if (!string.IsNullOrEmpty(prm.search))
            {
                query = query.Where(x =>
                    x.ZRCONO.Contains(prm.search) ||
                    x.ZRBRNO.Contains(prm.search) ||
                    x.ZRVANO.Contains(prm.search) ||
                    x.ZRVANA.Contains(prm.search)
                );
            }




            return await PagedList<ZVAR>.CreateAsync(query, prm.PageNumber, prm.PageSize);
        }
    }
}
