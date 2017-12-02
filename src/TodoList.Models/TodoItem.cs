using System;
using MongoDB.Bson;

namespace TodoList.Models
{
    public class TodoItem
    {
        public ObjectId _id { get; set;}
        
        public string Description { get; set; }
        
        public bool Completed { get; set; }
    }
}
