using ExperienceKeeper.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.DataSeed
{
    public class DataSeederProduction : IDataSeeder
    {
        private readonly ApplicationDbContext Context = null;

        public DataSeederProduction(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Seed()
        {

        }
    }
}
