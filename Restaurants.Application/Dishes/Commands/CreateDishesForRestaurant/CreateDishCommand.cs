using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Restaurants.Application.Dishes.Commands.CreateDishes;

public class CreateDishCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    //Restaurant Id
    [BindNever]
    public int RestaurantId { get; set; }
}
