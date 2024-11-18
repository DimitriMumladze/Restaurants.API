using MediatR;

namespace Restaurants.Application.Dishes.Commands.CreateDishes;

public class CreateDishCommand : IRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    //Restaurant Id
    public int RestaurantId { get; set; }
}
