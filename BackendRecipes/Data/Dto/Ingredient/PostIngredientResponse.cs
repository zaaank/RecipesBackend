using Newtonsoft.Json;
namespace BackendRecipes.Data.Dto.Ingredient
{
    public class PostIngredientResponse
    {
        public PostIngredientResponse(Entities.Ingredient ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;
            StoreName = ingredient.StoreName;
            Mark = ingredient.Mark;
        }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("storeName")]
        public string StoreName { get; set; }
        [JsonProperty("mark")]
        public string Mark { get; set; }
    }
}

