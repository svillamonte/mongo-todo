using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Repositories.Interfaces
{
    public interface ITodoItemRepository 
    {
        Task<TodoItem> GetTodoItem(string id);

        Task<TodoItem> InsertTodoItem(TodoItem todoItem);

        IEnumerable<TodoItem> GetTodoItems();
    }
}