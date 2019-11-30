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
        [HttpGet("defense/{type}")]
        public async Task<IActionResult> TypeChartAsDefender(string type)
        {
            var api = await PokeServiceApi.RequestData($"type/{type}");
            var results = JsonConvert.DeserializeObject<TypesModel>(api);

            var noEffect = new List<string>();
            var notVeryEffective = new List<string>();
            var superEffective = new List<string>();

            foreach (var r in results.DamageRelations.DoubleDamageFrom)
            {
                superEffective.Add(r.Name);
            }

            foreach (var r in results.DamageRelations.HalfDamageFrom)
            {
                notVeryEffective.Add(r.Name);
            }

            foreach (var r in results.DamageRelations.NoDamageFrom)
            {
                noEffect.Add(r.Name);
            }

            string json = JsonConvert.SerializeObject(new { superEffective, notVeryEffective, noEffect });
            return Ok(json);
        }

        [HttpGet("defense/{type1}/{type2}")]
        public async Task<IActionResult> DualTypeChartAsDefender(string type1, string type2)
        {
            var types = new List<string>();
            types.Add(type1);
            types.Add(type2);

            var noEffect = new List<string>();
            var notVeryEffectiveRaw = new List<string>();
            var superEffectiveRaw = new List<string>();

            foreach (var t in types)
            {
                var apiType = await PokeServiceApi.RequestData($"type/{t}");
                var resultsType = JsonConvert.DeserializeObject<TypesModel>(apiType);

                foreach (var rt in resultsType.DamageRelations.DoubleDamageFrom)
                {
                    superEffectiveRaw.Add(rt.Name);
                }

                foreach (var rt in resultsType.DamageRelations.HalfDamageFrom)
                {
                    notVeryEffectiveRaw.Add(rt.Name);
                }

                foreach (var rt in resultsType.DamageRelations.NoDamageFrom)
                {
                    noEffect.Add(rt.Name);
                }
            }

            var json = DualType.CalculateDamage(types, superEffectiveRaw, notVeryEffectiveRaw, noEffect);
            return Ok(json);

        }

        [HttpGet("offense/{type}")]
        public async Task<IActionResult> TypeChartAsAttacker (string type)
        {
            var api = await PokeServiceApi.RequestData($"type/{type}");
            var results = JsonConvert.DeserializeObject<TypesModel>(api);

            var noEffect = new List<string>();
            var notVeryEffective = new List<string>();
            var superEffective = new List<string>();

            foreach (var r in results.DamageRelations.DoubleDamageTo)
            {
                superEffective.Add(r.Name);
            }

            foreach (var r in results.DamageRelations.HalfDamageTo)
            {
                notVeryEffective.Add(r.Name);
            }

            foreach (var r in results.DamageRelations.NoDamageTo)
            {
                noEffect.Add(r.Name);
            }

            string json = JsonConvert.SerializeObject(new { noEffect, notVeryEffective, superEffective });

            return Ok(json);
        }

        [HttpGet("pokemon/{name}")]
        public async Task<IActionResult> PokemonTypeAsDefender(string name)
        {
            var api = await PokeServiceApi.RequestData($"pokemon/{name}");
            var results = JsonConvert.DeserializeObject<PokemonModel>(api);
            var types = new List<string>();
            var noEffect = new List<string>();
            var notVeryEffectiveRaw = new List<string>();
            var superEffectiveRaw = new List<string>();

            foreach (var r in results.Types)
            {
                types.Add(r.TypesData.Name);
                var apiType = await PokeServiceApi.RequestData($"type/{r.TypesData.Name}");
                var resultsType = JsonConvert.DeserializeObject<TypesModel>(apiType);

                foreach (var rt in resultsType.DamageRelations.DoubleDamageFrom)
                {
                    superEffectiveRaw.Add(rt.Name);
                }

                foreach (var rt in resultsType.DamageRelations.HalfDamageFrom)
                {
                    notVeryEffectiveRaw.Add(rt.Name);
                }

                foreach (var rt in resultsType.DamageRelations.NoDamageFrom)
                {
                    noEffect.Add(rt.Name);
                }
            }

            var json = DualType.CalculateDamage(types, superEffectiveRaw, notVeryEffectiveRaw, noEffect);
            return Ok(json);
        }
    }
}