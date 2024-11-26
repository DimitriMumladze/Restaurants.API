using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queris.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queris.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers;

[Controller]
[Route("api/restaurants")]
//[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    //[AllowAnonymous] 
    //That will be allowed without Authorize
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromRoute] GetAllRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);  
        return Ok(restaurants);
    }
    [HttpGet("{id}")]
    // Adding Claim basec access control
    [Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
    {
        var restaurants = await mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurants);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
            await mediator.Send(new DeleteRestaurantCommand(id));
        
            return NoContent();
    }

    [HttpPatch]
    [Authorize(Roles = UserRoles.Owner)] // Primary version of Authorization
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {
        User.IsInRole("String");// Second version of Authorization
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromRoute]int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

}
