using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendRecipes.Data.Entities;

namespace BackendRecipes.Data.Entities
{

    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Directions { get; set; }
        public string Group { get; set; }

        public ICollection<IngredientRecipe> Ingredients { get; set; }
    }
}