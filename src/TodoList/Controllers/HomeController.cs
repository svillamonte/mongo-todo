using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Repositories;
using TodoList.Services;
using TodoList.Services.Interfaces;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private static ITodoItemService _todoItemService;
        
        public HomeController(ITodoItemService todoItemService) 
        {
            _todoItemService = todoItemService;
        }
        
        public IActionResult Index()
        {
            var todoItems = _todoItemService.GetTodoItems();

            return View(MapToTodoItemModel(todoItems));
        }
        
        public IActionResult ActiveItems()
        {
            var todoItems = _todoItemService.GetActiveTodoItems();
                        
            return View(MapToTodoItemModel(todoItems));
        }

        [HttpGet]
        public IActionResult AddItem() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(TodoItemModel todoItemModel)
        {
            var todoItem = new TodoItem
            {
                Description = todoItemModel.Description
            };
            await _todoItemService.InsertTodoItem(todoItem);

            return View(new TodoItemModel());
        }

        public async Task<IActionResult> TickItem(string id) 
        {
            await _todoItemService.TickActiveTodoItem(id);

            return RedirectToAction("ActiveItems");
        }

        public IActionResult Error()
        {
            return View();
        }

        private IEnumerable<TodoItemModel> MapToTodoItemModel(IEnumerable<TodoItem> todoItems)
        {
            var todoItemModels = todoItems.Select(x => new TodoItemModel
            {
                Id = x._id.ToString(),
                Description = x.Description,
                Completed = x.Completed
            });

            return todoItemModels;
        }
    }
}
