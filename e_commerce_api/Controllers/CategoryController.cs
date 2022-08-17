using e_commerce_api.Api;
using e_commerce_api.Api.Dtos;
using e_commerce_api.Entities;
using e_commerce_api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;



namespace e_commerce_api.Controllers
{
    [Authorize]
    [ApiController]
    //Defining which Http route this controller will receive from
    [Route("categories")]
    public class CategoryController : ControllerBase
    {


        //Repository interface declaration
        private readonly ICategoriesRepository repository;


        public CategoryController(ICategoriesRepository repository)
        {
            //repository Initialization
            this.repository = repository;
        }

  [AllowAnonymous]
        //Get /categories
        [HttpGet]
        //Get categories function
        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = repository.GetCategoriesAsync().Select(Category => Category.AsCategoryDto());
            return categories;
        }


  [AllowAnonymous]
        //Get /categories/{id}
        [HttpGet("{id}")]
        //Get category by id method
        public ActionResult<CategoryDto> GetCategory(Guid id)
        {
            var category = repository.GetCategoryAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            return category.AsCategoryDto();
        }


        //POST /categories
        [HttpPost]
        public ActionResult<CreateCategoryDto> CreateCategory(CreateCategoryDto categoryDto)
        {
            Category category = new()
            {
                Id = Guid.NewGuid() ,
                Name = categoryDto.Name,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.CreateCategoryAsync(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category.AsCategoryDto());
        }

        //PUT /categories{id}
        [HttpPut("{id}")]
        public ActionResult<UpdateCategoryDto> UpdateCategory(Guid id, UpdateCategoryDto categoryDto)
        {
            var existingCategory = repository.GetCategoryAsync(id);

            if (existingCategory is null)
            {
                return NotFound();
            }
            Category updateCategory = existingCategory with
            {
                Name = categoryDto.Name
            };
            repository.UpdateCategoryAsync(updateCategory);
            return NoContent();

        }


        //DELETE /categories{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCategories(Guid id)
        {
            var existingCategory = repository.GetCategoryAsync(id);
            if (existingCategory is null)
            {
                return NotFound();
            }

            repository.DeleteCategoryAsync(id);
            return NoContent();

        }
    }
}