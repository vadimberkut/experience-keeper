using ExperienceKeeper.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.DataSeed
{
    public class DataSeederDevelopment : IDataSeeder
    {
        private readonly ApplicationDbContext Context = null;

        public DataSeederDevelopment(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Seed()
        {

        }
    }
}
