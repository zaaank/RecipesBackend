using BackendRecipes.Data.Entities;

namespace BackendRecipes.Data.Entities
{

    public class Recipe
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Directions { get; set; }
        public string Group { get; set; }

        public ICollection<IngredientRecipe> Ingredients { get; set; }
    }
}