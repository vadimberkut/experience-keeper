using ExperienceKeeper.Data.DataSeed;
using ExperienceKeeper.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Checks if all migrations were applied to DB
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsAllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Count;

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Count;

            return applied == total;
        }

        /// <summary>
        /// Ensures that data was seeded to DB (for development)
        /// </summary>
        /// <param name="context"></param>
        public static void EnsureSeedDevelopment(this ApplicationDbContext context)
        {
            IDataSeeder seeder = new DataSeederProduction(context);
            seeder.Seed();
        }

        /// <summary>
        /// Ensures that data was seeded to DB (for production)
        /// </summary>
        /// <param name="context"></param>
        public static void EnsureSeedProduction(this ApplicationDbContext context)
        {
            IDataSeeder seeder = new DataSeederDevelopment(context);
            seeder.Seed();
        }
    }
}
