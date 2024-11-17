using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommand() : IRequest<bool>
{
    [BindNever]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }
}
