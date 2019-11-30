using System.Threading.Tasks;

namespace PokePartner.Api.Services
{
    public interface IPokeApiService
    {
        Task<string> RequestData(string endpoint);
    }
}
