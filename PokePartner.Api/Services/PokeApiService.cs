using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokePartner.Api.Services
{
    public class PokeApiService : IPokeApiService
    {
        public async Task<string> RequestData(string endpoint) 
        {
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://pokeapi.co/api/v2/{endpoint}");
                var response = await client.GetAsync(url);
                var results = await response.Content.ReadAsStringAsync();
                return results;
            }
        }
    }
}