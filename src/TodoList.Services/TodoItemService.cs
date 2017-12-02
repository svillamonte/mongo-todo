using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<TodoItem> GetActiveTodoItems()
        {
            var todoItems = _todoItemRepository.GetTodoItems();

            return todoItems.Where(x => !x.Completed);
        }
    }
}
