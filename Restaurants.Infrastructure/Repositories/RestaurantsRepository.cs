using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext)
    : IRestaurantsRepository
{
    public async Task<int> Create(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Restaurant entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }
    //Pagination
    public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhase)
    {
        var searchPhaseLower = searchPhase?.ToLower();

        var restaurants = await dbContext
            .Restaurants
            .Where(r => searchPhaseLower == null || (r.Name.ToLower().Contains(searchPhaseLower)
                                                 || r.Description.ToLower().Contains(searchPhaseLower)))
            .ToListAsync();
        return restaurants;
    }
    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurants = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return restaurants;
    }

    public async Task SaveChanges() => await dbContext.SaveChangesAsync();
}