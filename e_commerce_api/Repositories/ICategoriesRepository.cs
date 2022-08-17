using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_api.Entities;

namespace e_commerce_api.Repositories
{
    public interface ICategoriesRepository
    {
        public Category GetCategoryAsync(Guid id);
     
        public IEnumerable<Category> GetCategoriesAsync();

        public void CreateCategoryAsync(Category category);

       public  void UpdateCategoryAsync(Category category);

       public void DeleteCategoryAsync(Guid id);
    }
}