using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokePartner.Api.Models;
using PokePartner.Api.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokePartner.Api.Controllers
{
    [Route("type")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        [HttpGet("{adjective}/{preposition}/{type}")]
        public async Task<IActionResult> TypeByName(string adjective, string preposition, string type)
        {

            // Question should be phrased as:
            // What is [STRONG/WEAK] against/to [TYPE].

            var api = await PokeServiceApi.RequestData($"type/{type}");
            var results = JsonConvert.DeserializeObject<TypesModel>(api);

            if (adjective == "strong" && preposition == "to")
            {
                var extraDamage = new List<string>();

                foreach (var r in results.DamageRelations.DoubleDamageFrom)
                {
                    extraDamage.Add(r.Name);
                }

                string json = JsonConvert.SerializeObject(new { extraDamage });


                return Ok(json);
            }
        }
    }
}