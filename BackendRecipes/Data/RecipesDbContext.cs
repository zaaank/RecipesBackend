using BackendRecipes.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendRecipes.API.Data
{
    public class RecipesDbContext : DbContext
    {
        public RecipesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientRecipe> IngredientRecipes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IngredientRecipe>(Entity => {
                Entity.HasKey(x => new { x.id });
                Entity.HasOne(x => x.Ingredient).WithMany(x => x.Recipes).HasForeignKey(x => x.IngredientId);
                Entity.HasOne(x => x.Recipe).WithMany(x => x.Ingredients).HasForeignKey(x => x.RecipeId);
            });
            base.OnModelCreating(modelBuilder);

        }
    }
}

