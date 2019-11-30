using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokePartner.Api.Models
{
    public class PokemonModel
    {
        [JsonProperty("types")]
        public List<TypesResult> Types { get; set; }
    }

    public class TypesResult
    {
        [JsonProperty("type")]
        public TypesData TypesData { get; set; }
    }

    public class TypesData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
