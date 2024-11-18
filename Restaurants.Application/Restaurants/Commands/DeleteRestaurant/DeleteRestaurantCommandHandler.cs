using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository) : 
    IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Restaurant with Id {RestaurantId}.", request.Id);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant == null)
            throw new NotFoundExceptions(nameof(Restaurant), request.Id.ToString()); //aqedan modis exception

        await restaurantsRepository.Delete(restaurant);
    }
}

