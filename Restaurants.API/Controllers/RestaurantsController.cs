using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers;

[Controller]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await restaurantsService.GetAllRestraurants();
        return Ok(restaurants);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var restaurants = await restaurantsService.GetById(id);
        if(restaurants is null)
        {
            return BadRequest();
        }
        return Ok(restaurants);
    }

    [HttpPatch]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        } 
        int id = await restaurantsService.Create(createRestaurantDto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

}
