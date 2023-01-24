using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private readonly string Company = "AMG";
        private readonly string Branch = "CKP";

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

        public IQueryable<IPTY> Query()
        {
            return _context.IPTY.AsQueryable();
        }

        public async Task<IPTY> FindByType(string palletType)
        {
            return await _context.IPTY.FirstOrDefaultAsync(d => d.HSCONO == Company && d.HSBRNO == Branch && d.HSPETY == palletType);
        }

        public async Task<IPTY> FindByUser(string user)
        {
            return await _context.IPTY.FirstOrDefaultAsync(d => d.HSCRUS == user);
        }

        public async Task<PagedList<IPTY>> All(PalletTypeParams param)
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
             .Where(c => c.HSCONO == Company && c.HSBRNO == Branch)
             .AsNoTracking()
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

            if (param.stp != null)
            {
                query = query.Where(c => c.HSRCST == param.stp);
            }

            // sort column
            if (param.srt != null)
            {
                switch (param.srt)
                {
                    case "plap":
                        query = query.OrderBy(c => c.HSPLAP);
                        break;
                    case "-plap":
                        query = query.OrderByDescending(c => c.HSPLAP);
                        break;
                    case "maty":
                        query = query.OrderBy(c => c.HSMATY);
                        break;
                    case "-maty":
                        query = query.OrderByDescending(c => c.HSMATY);
                        break;
                    case "pety":
                        query = query.OrderBy(c => c.HSPETY);
                        break;
                    case "-pety":
                        query = query.OrderByDescending(c => c.HSPETY);
                        break;
                    case "pena":
                        query = query.OrderBy(c => c.HSPENA);
                        break;
                    case "-pena":
                        query = query.OrderByDescending(c => c.HSPENA);
                        break;
                    case "unpr":
                        query = query.OrderBy(c => c.HSUNPR);
                        break;
                    case "-unpr":
                        query = query.OrderByDescending(c => c.HSUNPR);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(c => c.HSCRDT);
            }

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
            }
            else
            {
                findType = "CLNO";
            }

            return await _context.GCT2
            .Where(c => c.CBTBNO == findType)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<IList<ZVAR>> GetPalletAppDefinition()
        {
            return await _context.ZVAR
            .Where(c => c.ZRVATY == "PLAP")
             .AsNoTracking()
            .ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(string type, IPTY data)
        {
            /* _context.Attach(data);

            var entry = _context.Entry(data);
            entry.State = EntityState.Modified;
           
            // Prevent null fields from updating
            Type iptyType = entry.Entity.GetType();
            PropertyInfo[] properties = iptyType.GetProperties();

            foreach (var property in properties)
            {
                // check property Type
                if (property.PropertyType.Name == typeof(decimal).Name)
                {
                    // check null value on number property
                    if ((decimal)property.GetValue(entry.Entity, null) == 0)
                    {
                        entry.Property(property.Name).IsModified = false;
                    }
                }
                else
                {
                    // check null value on property
                    if (property.GetValue(data, null) == null)
                    {
                        entry.Property(property.Name).IsModified = false;
                    }
                }


            } */

            var changeTracking = _context.Update(data);

            if (changeTracking.State != EntityState.Modified)
            {
                return false;
            }

            return await SaveAll();
        }

        public async Task<IList<IUOM>> GetMeasurements()
        {
            return await _context.IUOM.AsNoTracking().ToListAsync();
        }

        public async Task<IList<IPTY>> Exportable(PalletTypeParams param)
        {
            // string queryRawString = 
            // "SELECT I.HSPETY, I.HSPENA, I.HSPLAP, I.HSMATY, " +
            // "I.HSPELN, I.HSPEWD, I.HSPEHG, I.HSPEWG, I.HSRCST, G.CBKYNA AS HSCLNO " +
            // "FROM IPTY AS I INNER JOIN GCT2 AS G ON G.CBKYNO = I.HSCLNO WHERE G.CBTBNO = 'CLNO' " +
            // $"AND I.HSCONO = '{Company}' AND I.HSBRNO = '{Branch}'";
            var query = from i in _context.IPTY
                        join g in _context.GCT2 on i.HSCLNO equals g.CBKYNO
                        select new IPTY
                        {
                            HSPETY = i.HSPETY,
                            HSPENA = i.HSPENA,
                            HSPLAP = i.HSPLAP,
                            HSMATY = i.HSMATY,
                            HSCLNO = g.CBKYNA,
                            HSPELN = i.HSPELN,
                            HSPEWD = i.HSPEWD,
                            HSPEHG = i.HSPEHG,
                            HSPEWG = i.HSPEWG,
                            HSRCST = i.HSRCST
                        };

            /* var gctlist = await GetCommonnText2("col");

            var raw = query.Join(gctlist, i => i.HSCLNO, c => c.CBKYNO, (i, c) => new
            {
                HSPETY = i.HSPETY,
                HSPENA = i.HSPENA,
                HSPLAP = i.HSPLAP,
                HSMATY = i.HSMATY,
                HSCLNO = c.CBKYNA,
                HSPELN = i.HSPELN,
                HSPEWD = i.HSPEWD,
                HSPEHG = i.HSPEHG,
                HSPEWG = i.HSPEWG,
                HSRCST = i.HSRCST
            }
            );

            var test = await raw.ToListAsync(); */

            // individual column search
            if (!string.IsNullOrEmpty(param.ptp))
            {
                // queryRawString = queryRawString + $"AND I.HSPETY = '{param.ptp}' ";
            }

            if (!string.IsNullOrEmpty(param.atp))
            {
                // queryRawString = queryRawString + $"AND I.HSPLAP = '{param.atp}' ";
            }

            if (!string.IsNullOrEmpty(param.mtp))
            {
                // queryRawString = queryRawString + $"AND I.HSMATY = '{param.mtp}' ";
            }

            if (param.stp != null)
            {
                // queryRawString = queryRawString + $"AND I.HSRCST = {param.stp} ";
            }

            // sort column
            /* if (param.srt != null)
            {
                switch (param.srt)
                {
                    case "plap":
                        queryRawString = queryRawString + $"ORDER BY I.HSPLAP ";
                        break;
                    case "-plap":
                        queryRawString = queryRawString + $"ORDER BY I.HSPLAP DESC ";
                        break;
                    case "maty":
                        queryRawString = queryRawString + $"ORDER BY I.HSMATY ";
                        break;
                    case "-maty":
                        queryRawString = queryRawString + $"ORDER BY I.HSMATY DESC ";
                        break;
                    case "pety":
                        queryRawString = queryRawString + $"ORDER BY I.HSPETY ";
                        break;
                    case "-pety":
                        queryRawString = queryRawString + $"ORDER BY I.HSPETY DESC ";
                        break;
                    case "pena":
                        queryRawString = queryRawString + $"ORDER BY I.HSPENA ";
                        break;
                    case "-pena":
                        queryRawString = queryRawString + $"ORDER BY I.HSPENA DESC ";
                        break;
                    case "unpr":
                        queryRawString = queryRawString + $"ORDER BY I.HSUNPR ";
                        break;
                    case "-unpr":
                        queryRawString = queryRawString + $"ORDER BY I.HSUNPR DESC ";
                        break;
                    default:
                        break;
                }
            } */

            return await query.ToListAsync();
        }
    }
}