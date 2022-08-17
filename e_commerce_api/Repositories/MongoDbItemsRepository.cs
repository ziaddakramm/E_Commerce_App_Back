using e_commerce_api.Entities;
using e_commerce_api.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Docker run command
//docker run -d --rm --name MyMongo -p 27017:27017 -v mongodbdata:/data/db mongo
//27017:27017 => open some port in the machine to be mapped to the docker container

namespace e_commerce_api.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {


        private const string databaseName = "catalog";

        private const string collectionName = "items";

        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder= Builders<Item>.Filter;


        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            //Getting a reference to the database
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);

            //reference to the collection
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
 



        public void CreateItemAsync(Item item)
        {
          itemsCollection.InsertOne(item); 
        }

        public void DeleteItemAsync(Guid id)
        {
         var filter = filterBuilder.Eq(existingItem => existingItem.Id, id);
         itemsCollection.DeleteOne(filter);
         
        }

        public  Item GetItemAsync(Guid id)
        {
                var filter = filterBuilder.Eq(item => item.Id, id);
           return  itemsCollection.Find(filter).SingleOrDefault();
       
        }

        public IEnumerable<Item> GetItemsAsync()
        {
               return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
             itemsCollection.ReplaceOne(filter,item);
        }

        
        public IEnumerable<Item> GetItemByCategoryAsync(String itemCategory)
        {
           var filter = filterBuilder.Eq(item => item.ItemCategory,itemCategory);
           return  itemsCollection.Find(filter).ToList();
        }

    }
}