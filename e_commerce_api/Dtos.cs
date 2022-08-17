using System;
using System.ComponentModel.DataAnnotations;
using e_commerce_api.Entities;

namespace e_commerce_api.Api.Dtos
{
public record ItemDto(Guid Id, string Name,string Description,decimal Price,string ItemCategory, DateTimeOffset CreatedDate);
public record CreateItemDto([Required] string Name, string Description,[Range(1,1000)]decimal Price,string ItemCategory);

public record UpdateItemDto([Required] string Name, [Range(1,1000)]decimal Price);

public record CategoryDto(Guid Id, string Name, DateTimeOffset CreatedDate);
public record CreateCategoryDto([Required] string Name);

public record UpdateCategoryDto([Required] string Name);

public record TokenDto(string token);
}