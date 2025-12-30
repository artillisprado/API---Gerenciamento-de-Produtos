using Microsoft.EntityFrameworkCore;
using MinhaApiCrud.Domain.Entities;
using MinhaApiCrud.Domain.Interfaces;
using MinhaApiCrud.Infrastructure.Data;

namespace MinhaApiCrud.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetByFiltersAsync(
        string? name = null,
        string? category = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        bool? inStock = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(p => p.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category == category);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (inStock.HasValue && inStock.Value)
            query = query.Where(p => p.Stock > 0);

        query = query.Where(p => p.IsActive);

        return await query
            .OrderBy(p => p.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}