using System;
namespace BackendRecipes.Data.Dto.Recipe
{
    public class GetRecipeByGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Directions { get; set; }
        public ICollection<int> ingredientIds { get; set; }
    }
}

