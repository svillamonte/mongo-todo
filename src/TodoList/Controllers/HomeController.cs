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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TodoItemModel todoItemModel)
        {
            var todoItem = new TodoItem
            {
                Description = todoItemModel.Description
            };
            await _todoItemService.InsertTodoItem(todoItem);

            return View(new TodoItemModel());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
