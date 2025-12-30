using MinhaApiCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaApiCrud.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByFiltersAsync(
        string? name = null,
        string? category = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        bool? inStock = null,
        int pageNumber = 1,
        int pageSize = 10
    );
}
