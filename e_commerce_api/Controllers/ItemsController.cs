using e_commerce_api.Api;
using e_commerce_api.Api.Dtos;
using e_commerce_api.Entities;
using e_commerce_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce_api.Controllers
{

    [Authorize]
    [ApiController]
    //Defining which Http route this controller will receive from
    [Route("items")]
    public class ItemsController : ControllerBase
    {

       // private readonly IJwtAuth jwtAuth;

        //Repository interface declaration
        private readonly IItemsRepository repository;


        public ItemsController(IItemsRepository repository/*, IJwtAuth jwtAuth*/)

        {
            //repository Initialization
            this.repository = repository;

            //Auth initialization
           // this.jwtAuth = jwtAuth;

        }


        [AllowAnonymous]
        //Get /items
        [HttpGet]
        //Get items function
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItemsAsync().Select(item => item.AsDto());
            return items;
        }

        [AllowAnonymous]
        //Get /items/{id}
        [HttpGet("{id}")]
        //Get item by id method
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }


       /*[AllowAnonymous]
        //POST /items
        [HttpPost("{authentication}")]
        public IActionResult Authenticate(UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }*/

        //POST /items
        [HttpPost]
        public ActionResult<CreateItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid()
              ,
                Name = itemDto.Name
              ,
                Description = itemDto.Description,
                Price = itemDto.Price,
                ItemCategory = itemDto.ItemCategory,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());

        }

        //PUT /items{id}
        [HttpPut("{id}")]
        public ActionResult<CreateItemDto> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }
            Item updateItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            repository.UpdateItemAsync(updateItem);
            return NoContent();

        }


        //DELETE /items{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            repository.DeleteItemAsync(id);
            return NoContent();

        }


        [AllowAnonymous]
        //Get /items/{itemCategory}
        [HttpGet("category/{itemCategory}")]
        //Get item by id fmethod
        public IEnumerable<ItemDto>  GetItemCategory(string itemCategory)
        {
            /*Category existingCategory = new()
            {
                Id = Guid.NewGuid()
                      ,
                Name = itemCategory,
                CreatedDate = DateTimeOffset.UtcNow
            };*/
            var items = repository.GetItemByCategoryAsync(itemCategory).Select(item => item.AsDto());
            if (items is null)
            {
                return null;
            }
            return items;
        }
    }

}