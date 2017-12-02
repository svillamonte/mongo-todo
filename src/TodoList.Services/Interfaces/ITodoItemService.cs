using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<TodoItem> InsertTodoItem(TodoItem todoItem);
    }
}
