using System;
using BackendRecipes.API.Data;

namespace BackendRecipes.Data.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string storeName { get; set; }
        public string mark { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}

