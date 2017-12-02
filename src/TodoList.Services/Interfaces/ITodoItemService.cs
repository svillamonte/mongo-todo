using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<TodoItem> InsertTodoItem(TodoItem todoItem);

        IEnumerable<TodoItem> GetTodoItems();

        IEnumerable<TodoItem> GetActiveTodoItems();

        Task TickActiveTodoItem(string id);
    }
}
