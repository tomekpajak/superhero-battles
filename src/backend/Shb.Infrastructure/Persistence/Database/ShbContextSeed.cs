using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using Shb.Domain.Models;
using Shb.Infrastructure.Configurations;

namespace Shb.Infrastructure.Persistence.Database
{
    internal static class ShbContextSeed
    {
        public static IEnumerable<Superhero> GetSampleOfSuperheroes()  
        {
            return Task.Run(() => GetSampleOfSuperheroesAsync()).Result;
        }
        public static async Task<IEnumerable<Superhero>> GetSampleOfSuperheroesAsync() 
        {
            using (FileStream jsonFile = File.OpenRead(ShbInfrastructureConst.JsonFile)) 
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Superhero>>(jsonFile);
            }           
        }
    }
}
