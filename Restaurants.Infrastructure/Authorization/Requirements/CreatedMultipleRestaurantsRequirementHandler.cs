using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

internal class CreatedMultipleRestaurantsRequirementHandler(IRestaurantsRepository restaurantsRepository,
    IUserContext userContext) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CreatedMultipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser(); // Who is current user
         
        var restaurats = await restaurantsRepository.GetAllAsync(); // In restaurants we have
        // all the restaurants

        var userRestaurantsCreated = restaurats.Count(r => r.OwnerId == currentUser!.Id);
        // Integer of created restaurants by user

        if(userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
        {
            context.Succeed(requirement);
        } 
        else
        {
            context.Fail(); 
        }


    }
}
