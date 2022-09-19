using System;
namespace BackendRecipes.Data.Entities
{
    public class IngredientRecipe
    {
        public int id { get; set; }
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}

