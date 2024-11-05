namespace Restaurants.Domain.Entities;

public class Restaurant
{
    //Here is Entity of Identification, Restaurant Name, Restaurant Description
    //Restaurant Category, Restaurant have or nor Delivery
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    //Here is information of their Email and mobile Number
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    //Here is Their address and list of dishes
    public Address? Address { get; set; }
    public List<Dish> Dishes { get; set; } = [];
}
