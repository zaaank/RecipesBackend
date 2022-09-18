using System;
using System.Diagnostics.Metrics;
using AutoMapper;
using BackendRecipes.API.Data;
using BackendRecipes.Data.Dto.Ingredient;
using BackendRecipes.Data.Dto.Recipe;
using BackendRecipes.Data.Entities;

namespace HotelListing.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Ingredient, PostIngredient>().ReverseMap(); //Reverse map allow us to also map from dto to country
            CreateMap<Ingredient, GetAllIngredients>().ReverseMap();

            CreateMap<Recipe, AddRecipe>().ReverseMap();
        }
    }
}

