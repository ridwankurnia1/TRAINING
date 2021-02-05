using System.Threading.Tasks;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public interface IAuthRepository
    {
        Task<ZUSR> Login(string username, string password);
    }
}