using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokePartner.Api.Services
{
    public class TypesCalculator
    {
        public static string CalculateDamage(List<string> SuperEffective, List<string> NotVeryEffective, List<string> NoEffect)
        {

            // Calculate Damage Differences
            List<string> superEffective = SuperEffective.Distinct().ToList();
            IEnumerable<string> superEffectiveX4 = new List<string>();

            foreach (var type in SuperEffective.ToList())
            {
                // SuperEffective X4
                if (SuperEffective.Count != SuperEffective.Distinct().Count())
                {
                    superEffectiveX4 = SuperEffective.GroupBy(x => x)
                        .Where(group => group.Count() > 1)
                        .Select(group => group.Key);
                }
                // Clean Up SuperEffective X2
                if (NotVeryEffective.Contains(type))
                {
                    superEffective.Remove(type);
                }
            }

            // Build SuperEffective X2 List
            List<string> superEffectiveX2 = superEffective.Distinct().ToList();
            foreach (var type in superEffectiveX2.ToList())
            {
                if (superEffectiveX4.Contains(type))
                {
                    superEffectiveX2.Remove(type);
                }
            }

            List<string> notVeryEffective = NotVeryEffective.Distinct().ToList();
            IEnumerable<string> notVeryEffectiveQuarter = new List<string>();
            foreach (var type in NotVeryEffective.ToList())
            {
                // NotVeryEffective Quarter
                if (NotVeryEffective.Count != NotVeryEffective.Distinct().Count())
                {
                    notVeryEffectiveQuarter = NotVeryEffective.GroupBy(x => x)
                        .Where(group => group.Count() > 1)
                        .Select(group => group.Key);
                }
                // Clean Up NotVeryEffective Half
                if (SuperEffective.Contains(type))
                {
                    notVeryEffective.Remove(type);
                }
            }

            // Check NotVeryEffective
            List<string> notVeryEffectiveHalf = notVeryEffective.Distinct().ToList();
            foreach (var type in notVeryEffectiveHalf.ToList())
            {
                if (notVeryEffectiveQuarter.Contains(type))
                {
                    notVeryEffectiveHalf.Remove(type);
                }
            }

            foreach (var x in NoEffect)
            {
                superEffectiveX2.Remove(x);
                notVeryEffectiveHalf.Remove(x);
            }

            string json = JsonConvert.SerializeObject(new { superEffectiveX4, superEffectiveX2, notVeryEffectiveHalf, notVeryEffectiveQuarter, NoEffect });

            return json;
        }
    }
}
