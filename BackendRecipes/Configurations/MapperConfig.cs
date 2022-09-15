using System;
using System.Diagnostics.Metrics;
using AutoMapper;
using BackendRecipes.Data.Dto.Ingredient;
using BackendRecipes.Data.Entities;

namespace HotelListing.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Ingredient, PostIngredient>().ReverseMap(); //Reverse map allow us to also map from dto to country

        }
    }
}

