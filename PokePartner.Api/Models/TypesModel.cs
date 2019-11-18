using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokePartner.Api.Models
{
    public class TypesModel
    {
        [JsonProperty("damage_relations")]
        public Result DamageRelations { get; set; }
    }

    public class Result
    {
        [JsonProperty("double_damage_from")]
        public List<DamageData> DoubleDamageFrom { get; set; }

        [JsonProperty("double_damage_to")]
        public List<DamageData> DoubleDamageTo { get; set; }

        [JsonProperty("half_damage_from")]
        public List<DamageData> HalfDamageFrom { get; set; }

        [JsonProperty("half_damage_to")]
        public List<DamageData> HalfDamageTo { get; set; }

        [JsonProperty("no_damage_from")]
        public List<DamageData> NoDamageFrom { get; set; }

        [JsonProperty("no_damage_to")]
        public List<DamageData> NoDamageTo { get; set; }
    }

    public class DamageData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
