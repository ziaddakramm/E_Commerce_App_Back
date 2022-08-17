using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_api.Entities;

namespace e_commerce_api.Repositories
{
    public interface IItemsRepository
    {
        public Item GetItemAsync(Guid id);
        public IEnumerable<Item> GetItemByCategoryAsync(String itemCategory);
        public IEnumerable<Item> GetItemsAsync();

        void CreateItemAsync(Item item);

        void UpdateItemAsync(Item item);

        void DeleteItemAsync(Guid id);
    }
}