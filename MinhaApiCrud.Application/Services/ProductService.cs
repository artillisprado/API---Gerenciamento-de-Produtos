using MinhaApiCrud.Application.DTOs;
using MinhaApiCrud.Domain.Entities;
using MinhaApiCrud.Domain.Interfaces;

namespace MinhaApiCrud.Application.Services;

public interface IProductService
{
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<IEnumerable<ProductDto>> GetByFiltersAsync(ProductFilterDto filter);
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto);
    Task<bool> DeleteAsync(Guid id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product == null ? null : MapToDto(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductDto>> GetByFiltersAsync(ProductFilterDto filter)
    {
        var products = await _repository.GetByFiltersAsync(
            filter.Name,
            filter.Category,
            filter.MinPrice,
            filter.MaxPrice,
            filter.InStock,
            filter.PageNumber,
            filter.PageSize
        );
        return products.Select(MapToDto);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            Category = dto.Category
        };

        var created = await _repository.AddAsync(product);
        return MapToDto(created);
    }

    public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return null;

        if (!string.IsNullOrWhiteSpace(dto.Name))
            product.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            product.Description = dto.Description;

        if (dto.Price.HasValue)
            product.Price = dto.Price.Value;

        if (dto.Stock.HasValue)
            product.Stock = dto.Stock.Value;

        if (!string.IsNullOrWhiteSpace(dto.Category))
            product.Category = dto.Category;

        await _repository.UpdateAsync(product);
        return MapToDto(product);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}