using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Entities.Relations;
using System.Reflection.Emit;

namespace Data
{
    public class ServiceContext : DbContext
    {
        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options) { }
        public DbSet<AlergenItem> Alergens { get; set; }
        public DbSet<RecipeItem> Recipes { get; set; }
        public DbSet<IngredientItem> Ingredients { get; set; }
        public DbSet<CategoryItem> Categories { get; set; }
        public DbSet<UserItem> Users { get; set; }
        public DbSet<UserRolItem> RolType { get; set; }
        public DbSet<OrderItem> Orders { get; set; }
        public DbSet<Recipe_Alergen> Recipe_Alergens { get; set; }
        public DbSet<Recipe_Ingredient> Recipe_Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RecipeItem>(entity => {
                entity.ToTable("Recipes");
                builder.Entity<RecipeItem>().HasKey(p => p.Id);
                entity.HasOne<UserItem>().WithMany().HasForeignKey(r => r.PostedBy);


            });

            builder.Entity<AlergenItem>(entity => {
                entity.ToTable("Alergens");
                builder.Entity<AlergenItem>().HasKey(p => p.Id);
            });

            builder.Entity<IngredientItem>(entity => {
                entity.ToTable("Ingredients");
                builder.Entity<IngredientItem>().HasKey(p => p.Id);
                entity.HasMany(i => i.Order)
                    .WithOne(o => o.Ingredient)
                    .HasForeignKey(o => o.IdIngredient);

            });

            builder.Entity<CategoryItem>(entity => {
                entity.ToTable("Categories");
                builder.Entity<CategoryItem>().HasKey(p => p.Id);
            });

            builder.Entity<UserItem>(entity => {
                entity.ToTable("Users");
                builder.Entity<UserItem>().HasKey(p => p.Id);
                entity.HasOne<UserRolItem>().WithMany().HasForeignKey(u => u.IdRol);
            });

            builder.Entity<UserRolItem>(entity => {
                entity.ToTable("RolType");
                builder.Entity<UserRolItem>().HasKey(ur => ur.Id);
               
            });

            builder.Entity<OrderItem>(entity => {
                entity.ToTable("Orders");
                builder.Entity<OrderItem>().HasKey(p => p.Id);
                entity.HasOne(o => o.Ingredient)
                .WithMany(i => i.Order)
                .HasForeignKey(o => o.IdIngredient);

                builder.Entity<OrderItem>()
    .HasOne(o => o.User)
    .WithMany(u => u.Order)
    .HasForeignKey(o => o.IdUser);

            });

            builder.Entity<Recipe_Alergen>(entity => {
                entity.ToTable("Recipe_Alergens");
                builder.Entity<Recipe_Alergen>().HasKey(p => p.Id);
                builder.Entity<Recipe_Alergen>().HasOne(ra => ra.Recipes)
                              .WithMany(r => r.Alergens)
                              .HasForeignKey(ra => ra.RecipeId);
                builder.Entity<Recipe_Alergen>().HasOne(ra => ra.Alergens)
                             .WithMany(a => a.Alergens)
                             .HasForeignKey(ra => ra.AlergenId);

            });

            builder.Entity<Recipe_Ingredient>(entity => {
                entity.ToTable("Recipe_Ingredients");
                builder.Entity<Recipe_Ingredient>().HasKey(p => p.Id);

                builder.Entity<Recipe_Ingredient>().HasOne(ri => ri.Recipes)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(ri => ri.RecipeId);

                builder.Entity<Recipe_Ingredient>()
                .HasOne(ri => ri.Ingredients)
                .WithMany(i => i.Ingredients)
                .HasForeignKey(ri => ri.IngredientId);

            });

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }
    }

    public class ServiceContextFactory : IDesignTimeDbContextFactory<ServiceContext>
    {
        public ServiceContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", false, true);
            var config = builder.Build();
            var connectionString = config.GetConnectionString("ServiceContext");
            var optionsBuilder = new DbContextOptionsBuilder<ServiceContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("ServiceContext"));
            optionsBuilder.EnableSensitiveDataLogging();

            return new ServiceContext(optionsBuilder.Options);
        }
    }
}
