using System;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Repositories.Interfaces;
using TodoList.Services.Interfaces;

namespace TodoList.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository) 
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<TodoItem> InsertTodoItem(TodoItem todoItem)
        {
            return await _todoItemRepository.InsertTodoItem(todoItem);
        }
    }
}
