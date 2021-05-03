using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class ORDSRepository : IORDSRepository
    {
        private readonly ORDSContext _context;
        public ORDSRepository(ORDSContext context)
        {
            _context = context;
        }
        public async Task<KTPA02> KTPA02Get(string documentNo)
        {
            // IBM.Data.Db2.DB2Connection 
            
            var isConnect = _context.Database.CanConnect();
            
            return await _context.KTPA02.FirstOrDefaultAsync(x => x.DONOA02 == documentNo);
        }
    }
}