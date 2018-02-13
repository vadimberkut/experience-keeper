using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optsBuilder)
        {
            // The connection string can be added in the OnConfiguring method in the class which implements the DbContext, 
            // or via dependency injection in the constructor using the options.
            base.OnConfiguring(optsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        //public override int SaveChanges()
        //{
        //    return base.SaveChanges();
        //}
    }
}
