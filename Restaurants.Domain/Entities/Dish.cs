namespace Restaurants.Domain.Entities;

public class Dish
{
    //Here is entity of dishes, their Id, name, description and price
    public int Id { get; set; } 
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    //id of the restaurant
    public int RestaurantId { get; set; }
}
