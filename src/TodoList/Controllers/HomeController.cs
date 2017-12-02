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
            try 
            {
                var todoItems = _todoItemService.GetTodoItems();

                return View(MapToTodoItemModel(todoItems));
            }
            catch
            {
                return View(new TodoItemModel[0]);
            }            
        }
        
        public IActionResult ActiveItems()
        {
            try 
            {
                var todoItems = _todoItemService.GetActiveTodoItems();
                        
                return View(MapToTodoItemModel(todoItems));
            }
            catch
            {
                return View(new TodoItemModel[0]);
            }            
        }

        [HttpGet]
        public IActionResult AddItem() 
        {
            return View(new TodoItemModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(TodoItemModel todoItemModel)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    Description = todoItemModel.Description
                };
                await _todoItemService.InsertTodoItem(todoItem);

                return View(new TodoItemModel());
            }
            catch
            {
                return View(new TodoItemModel { ShowError = true });
            }            
        }

        public async Task<IActionResult> TickItem(string id) 
        {
            try 
            {
                await _todoItemService.TickActiveTodoItem(id);

                return RedirectToAction("ActiveItems");
            }
            catch
            {
                return RedirectToAction("ActiveItems");
            }            
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
