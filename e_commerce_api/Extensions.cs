
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce_api.Entities;
using e_commerce_api.Api.Dtos;
namespace e_commerce_api.Api
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id,item.Name,item.Description,item.Price,item.ItemCategory,item.CreatedDate);
        }

    public static CategoryDto AsCategoryDto(this Category category)
        {
            return new CategoryDto(category.Id,category.Name,category.CreatedDate);
        }
       
    }
}
