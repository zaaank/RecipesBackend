using System;
namespace BackendRecipes.Data.Dto.Recipe
{
    public class AddRecipe
    {
        public string Name { get; set; }
        public string Directions { get; set; }
        public string Group { get; set; }

        public List<int> IngredientIds { get; set; }
    }
}

