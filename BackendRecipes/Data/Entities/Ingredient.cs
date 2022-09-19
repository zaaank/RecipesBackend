using System;
using BackendRecipes.Data.Entities;

namespace BackendRecipes.Data.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string Mark { get; set; }

        public ICollection<IngredientRecipe> Recipes { get; set; }
    }
}

