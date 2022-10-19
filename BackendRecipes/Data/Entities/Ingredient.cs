using System;
using BackendRecipes.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendRecipes.Data.Entities
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string Mark { get; set; }

        public ICollection<IngredientRecipe> Recipes { get; set; }
    }
}

