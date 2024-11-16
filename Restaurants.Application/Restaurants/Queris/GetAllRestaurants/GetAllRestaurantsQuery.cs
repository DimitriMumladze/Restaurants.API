using MediatR;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queris.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
{

}
