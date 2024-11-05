namespace Restaurants.Domain.Entities;

public class Address
{
    //Entity of where is restaurant [city, street, postal code]
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
