using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Helper;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AMGContext _context;
        public AuthRepository(AMGContext context)
        {
            _context = context;
        }

        public async Task<ZUSR> Login(string username, string password)
        {
            var user = await _context.ZUSR.FirstOrDefaultAsync(x => x.ZUGCEP == username);
            if (user == null)
            {
                return null;
            }

            ActiveDirectory AgcJp = new ActiveDirectory();
            bool isExists = AgcJp.IsUserExisiting(username);
            if (isExists)
            {
                if (AgcJp.ValidateCredentials(username, password))
                {
                    return user;
                }
            }

            return null;
        }
    }
}