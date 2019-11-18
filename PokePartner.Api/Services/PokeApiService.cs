using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokePartner.Api.Services
{
    public class PokeServiceApi
    {
        public static async Task<string> RequestData(String endpoint)
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