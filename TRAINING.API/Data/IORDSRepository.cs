using System.Threading.Tasks;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public interface IORDSRepository
    {
        Task<KTPA02> KTPA02Get(string documentNo);
    }
}