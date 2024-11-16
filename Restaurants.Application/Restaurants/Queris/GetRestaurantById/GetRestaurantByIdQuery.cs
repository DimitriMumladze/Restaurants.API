using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using System.ComponentModel;

namespace Restaurants.Application.Restaurants.Queris.GetRestaurantById;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto?>
{
    public int Id { get; } = id;
}
