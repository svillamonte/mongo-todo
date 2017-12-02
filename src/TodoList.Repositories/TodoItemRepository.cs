using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoList.Models;
using TodoList.Repositories.Interfaces;

namespace TodoList.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IMongoCollection<TodoItem> _collection;

        public TodoItemRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TodoItem>("items");
        }

        public async Task<TodoItem> GetTodoItem(string id) 
        {
            var bsonDocument = new BsonDocument {
                { "_id", new ObjectId(id) }
            };

            return await _collection.Find(bsonDocument).FirstAsync();
        }

        public async Task<TodoItem> InsertTodoItem(TodoItem todoItem) 
        {
            await _collection.InsertOneAsync(todoItem);
            return await GetTodoItem(todoItem._id.ToString());
        }
    }
}
