using System;
using System.Collections.Generic;
using e_commerce_api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace e_commerce_api.Repositories
{
    public class MongoDbCategoryRepository : ICategoriesRepository
    {

        private const string databaseName = "catalog";

        private const string collectionName = "categories";

        private readonly IMongoCollection<Category> categoriesCollection;

        private readonly FilterDefinitionBuilder<Category> filterBuilder = Builders<Category>.Filter;


        public MongoDbCategoryRepository(IMongoClient mongoClient)
        {
            //Getting a reference to the database
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);

            //reference to the collection
            categoriesCollection = database.GetCollection<Category>(collectionName);
        }


        public Category GetCategoryAsync(Guid id)
        {
            var filter = filterBuilder.Eq(Category => Category.Id, id);
            return categoriesCollection.Find(filter).SingleOrDefault();

        }

        public IEnumerable<Category> GetCategoriesAsync()
        {
            return categoriesCollection.Find(new BsonDocument()).ToList();

        }

        public void CreateCategoryAsync(Category category)
        {
            categoriesCollection.InsertOne(category);
        }

        public void UpdateCategoryAsync(Category category)
        {
            var filter = filterBuilder.Eq(existingCategory => existingCategory.Id, category.Id);
            categoriesCollection.ReplaceOne(filter, category);
        }

        public void DeleteCategoryAsync(Guid id)
        {
            var filter = filterBuilder.Eq(existingCategory => existingCategory.Id, id);
            categoriesCollection.DeleteOne(filter);
        }
    }
}