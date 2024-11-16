using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queris.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queris.GetRestaurantById;
using Restaurants.Domain.Repositories;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers;

[Controller]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var restaurants = await mediator.Send(new GetRestaurantByIdQuery(id));
 
        if(restaurants is null)
        {
            return BadRequest();
        }
        return Ok(restaurants);
    }

    [HttpPatch]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

}
