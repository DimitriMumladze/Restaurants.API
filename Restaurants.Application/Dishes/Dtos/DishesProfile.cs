using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDishes;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        //added while creating the --handler
        CreateMap<CreateDishCommand, Dish>();
        //
        CreateMap<Dish, DishDto>();
    }
}
