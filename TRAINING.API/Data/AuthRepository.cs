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

        public async Task<XUSR> LoginX(string username, string password)
        {
            if (username.IndexOf(".") >= 0)
            {
                var user = await _context.XUSR.FirstOrDefaultAsync(x => x.XUGCEP == username);
                if (user == null)
                    return null;

                user.XUREMA = "";
                return user;
            }
            else 
            {
                var user = await _context.XUSR.FirstOrDefaultAsync(x => x.XUUSNO == username);

                if (user == null)
                    return null;

                if (!VerifyPasswordHash(password, user.XUHASH, user.XUPSWD))
                {
                    user.XUREMA = "Password invalid";
                }
                
                user.XUREMA = "";
                return user;
            }           
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
    }
}