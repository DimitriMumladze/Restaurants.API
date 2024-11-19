using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queris.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantCommandHandler(ILogger<GetDishByIdForRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetDishByIdForRestaurantCommand, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation
            ("Retriving dish: {DishId}, for restaurant with id {RestaurantId}", request.DishId, request.RestaurantId);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.DishId.ToString());

        var results = mapper.Map<DishDto>(dish);

        return results;

    }
}
