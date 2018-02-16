using ExperienceKeeper.Data.Models;
using ExperienceKeeper.Data.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperienceKeeper.Data.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<Record> ExperienceRecords { get; set; }
        public DbSet<RecordUserCategory> ExperienceRecordUserCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optsBuilder)
        {
            // The connection string can be added in the OnConfiguring method in the class which implements the DbContext, 
            // or via dependency injection in the constructor using the options.
            base.OnConfiguring(optsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // PG doesn't have built in function to generate UUID, so if you 
            // want use UUIDs as promary keys you should install additional
            // modules or generate it in code
            const string PG_UUID_GENERATE_COMMAND = "<NO SUCH COMMAND>";
            const string PG_UUID_TYPE_NAME = "uuid";
            const string PG_TIMESTAMP_TYPE_NAME = "timestamp";
            const string PG_TIMESTAMP_UTC_GENERATE_COMMAND = "CAST(NOW() at time zone 'utc' AS timestamp)";

            // Take length considering storing: 
            // GUID 38 chars (not use 68 chars Hexadecimal format), 
            // UUID 36 chars, but can be 39 in some of alternative forms 
            // Ulid 26 chars
            const string ID_FILED_TYPE = "character varying(39)"; 

            // ApplicationUser
            builder.Entity<ApplicationUser>()
                .HasKey(x => x.Id);

            builder.Entity<ApplicationUser>()
                .Property(x => x.Id)
                .HasColumnType(ID_FILED_TYPE)
                .IsRequired();


            builder.Entity<ApplicationUser>()
                .Property(u => u.CreatedOnUtc)
                .HasColumnType(PG_TIMESTAMP_TYPE_NAME)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(PG_TIMESTAMP_UTC_GENERATE_COMMAND);

            builder.Entity<ApplicationUser>()
                .Property(u => u.UpdatedOnUtc)
                .HasColumnType(PG_TIMESTAMP_TYPE_NAME)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql(PG_TIMESTAMP_UTC_GENERATE_COMMAND);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.ExperienceRecords)
                .WithOne(er => er.User)
                .HasForeignKey(er => er.UserId)
                .IsRequired();

            // ExperienceRecord
            builder.Entity<Record>()
                .HasKey(x => x.Id);

            builder.Entity<Record>()
                .Property(x => x.Id)
                .HasColumnType(ID_FILED_TYPE)
                .IsRequired();

            builder.Entity<Record>()
                .Property(r => r.CreatedOnUtc)
                .HasColumnType(PG_TIMESTAMP_TYPE_NAME)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(PG_TIMESTAMP_UTC_GENERATE_COMMAND);

            builder.Entity<Record>()
                .Property(r => r.UpdatedOnUtc)
                .HasColumnType(PG_TIMESTAMP_TYPE_NAME)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql(PG_TIMESTAMP_UTC_GENERATE_COMMAND);

            // User-Category - Many to many
            builder.Entity<UserCategory>()
                .HasKey(uc => uc.Id);

            builder.Entity<UserCategory>()
                .Property(x => x.Id)
                .HasColumnType(ID_FILED_TYPE)
                .IsRequired();

            builder.Entity<UserCategory>()
                .Property(uc => uc.CreatedOnUtc)
                .HasColumnType(PG_TIMESTAMP_TYPE_NAME)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(PG_TIMESTAMP_UTC_GENERATE_COMMAND);

            builder.Entity<UserCategory>()
                .Property(uc => uc.UpdatedOnUtc)
                .HasColumnType(PG_TIMESTAMP_TYPE_NAME)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql(PG_TIMESTAMP_UTC_GENERATE_COMMAND);


            builder.Entity<UserCategory>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCategories)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            builder.Entity<UserCategory>()
                .HasOne(uc => uc.Category)
                .WithMany(c => c.UserCategories)
                .HasForeignKey(uc => uc.CategoryId)
                .IsRequired();

            // Record-UserCategory - Many to many
            builder.Entity<RecordUserCategory>()
                .HasKey(eruc => new { eruc.RecordId, eruc.UserCategoryId });

            builder.Entity<RecordUserCategory>()
                .HasOne(eruc => eruc.Record)
                .WithMany(er => er.RecordUserCategories)
                .HasForeignKey(eruc => eruc.RecordId)
                .IsRequired();

            builder.Entity<RecordUserCategory>()
                .HasOne(eruc => eruc.UserCategory)
                .WithMany(uc => uc.RecordUserCategories)
                .HasForeignKey(eruc => eruc.UserCategoryId)
                .IsRequired();

            // Category
            builder.Entity<Category>()
                .HasKey(x => x.Id);

            builder.Entity<Category>()
                .Property(x => x.Id)
                .HasColumnType(ID_FILED_TYPE)
                .IsRequired();

            builder.Entity<Category>()
                .Property(c => c.CreatedOnUtc)
                .HasColumnType(PG_TIMESTAMP_TYPE_NAME)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(PG_TIMESTAMP_UTC_GENERATE_COMMAND);

            builder.Entity<Category>()
                .Property(c => c.UpdatedOnUtc)
                .HasColumnType(PG_TIMESTAMP_TYPE_NAME)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql(PG_TIMESTAMP_UTC_GENERATE_COMMAND);
        }

        //public override int SaveChanges()
        //{
        //    return base.SaveChanges();
        //}
    }
}
