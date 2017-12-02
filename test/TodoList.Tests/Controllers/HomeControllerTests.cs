using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.Controllers;
using TodoList.Models;
using TodoList.Services;
using TodoList.Services.Interfaces;
using Xunit;

namespace TodoList.Tests
{
    public class HomeControllerTests
    {
        private Mock<ITodoItemService> _mockTodoItemService;
        private readonly HomeController _homeController;

        public HomeControllerTests() 
        {
            _mockTodoItemService = new Mock<ITodoItemService>();
            _homeController = new HomeController(_mockTodoItemService.Object);
        }

        [Fact]
        public void Index_WithoutExceptions_ReturnsViewModels()
        {
            // Assert
            var todoItemOne = new TodoItem { Description = "One", Completed = true };
            var todoItemTwo = new TodoItem { Description = "Two", Completed = false };
            var todoItemThree = new TodoItem { Description = "Three", Completed = false };

            _mockTodoItemService
                .Setup(x => x.GetTodoItems())
                .Returns(new [] { todoItemOne, todoItemTwo, todoItemThree });

            // Act
            var result = _homeController.Index() as ViewResult;

            // Assert
            var enumerable = (IEnumerable<TodoItemModel>)result.Model;

            Assert.Equal(todoItemOne.Description, enumerable.ElementAt(0).Description);
            Assert.Equal(todoItemTwo.Description, enumerable.ElementAt(1).Description);
            Assert.Equal(todoItemThree.Description, enumerable.ElementAt(2).Description);
        }

        [Fact]
        public void Index_WithExceptions_ReturnsEmptyList()
        {
            // Assert
            var todoItemOne = new TodoItem { Description = "One", Completed = true };
            var todoItemTwo = new TodoItem { Description = "Two", Completed = false };
            var todoItemThree = new TodoItem { Description = "Three", Completed = false };

            _mockTodoItemService
                .Setup(x => x.GetTodoItems())
                .Throws(new Exception());

            // Act
            var result = _homeController.Index() as ViewResult;

            // Assert
            var enumerable = (IEnumerable<TodoItemModel>)result.Model;
            Assert.False(enumerable.Any());
        }

        [Fact]
        public void ActiveItems_WithoutExceptions_ReturnsViewModels()
        {
            // Assert
            var todoItemOne = new TodoItem { Description = "One", Completed = false };
            var todoItemTwo = new TodoItem { Description = "Two", Completed = false };
            var todoItemThree = new TodoItem { Description = "Three", Completed = false };

            _mockTodoItemService
                .Setup(x => x.GetActiveTodoItems())
                .Returns(new [] { todoItemOne, todoItemTwo, todoItemThree });

            // Act
            var result = _homeController.ActiveItems() as ViewResult;

            // Assert
            var enumerable = (IEnumerable<TodoItemModel>)result.Model;

            Assert.Equal(todoItemOne.Description, enumerable.ElementAt(0).Description);
            Assert.Equal(todoItemTwo.Description, enumerable.ElementAt(1).Description);
            Assert.Equal(todoItemThree.Description, enumerable.ElementAt(2).Description);
        }

        [Fact]
        public void ActiveItems_WithExceptions_ReturnsEmptyList()
        {
            // Assert
            var todoItemOne = new TodoItem { Description = "One", Completed = false };
            var todoItemTwo = new TodoItem { Description = "Two", Completed = false };
            var todoItemThree = new TodoItem { Description = "Three", Completed = false };

            _mockTodoItemService
                .Setup(x => x.GetActiveTodoItems())
                .Throws(new Exception());

            // Act
            var result = _homeController.ActiveItems() as ViewResult;

            // Assert
            var enumerable = (IEnumerable<TodoItemModel>)result.Model;
            Assert.False(enumerable.Any());
        }

        [Fact]
        public void AddItem_HttpGet()
        {
            // Act
            var result = _homeController.AddItem() as ViewResult;

            // Assert
            var viewModel = (TodoItemModel)result.Model;
            Assert.Null(viewModel.Description);
            Assert.False(viewModel.Completed);
            Assert.False(viewModel.ShowError);
        }

        [Fact]
        public async Task AddItem_HttpPost_WithoutExceptions_ReturnsEmptyViewModel()
        {            
            // Assert
            var todoItemModel = new TodoItemModel();

            // Act
            var result = await _homeController.AddItem(todoItemModel) as ViewResult;

            // Assert
            var viewModel = (TodoItemModel)result.Model;            
            Assert.False(viewModel.ShowError);
        }

        [Fact]
        public async Task AddItem_HttpPost_WithExceptions_ReturnsViewModelWithError()
        {            
            // Assert
            var todoItemModel = new TodoItemModel();

            _mockTodoItemService
                .Setup(x => x.InsertTodoItem(It.IsAny<TodoItem>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _homeController.AddItem(todoItemModel) as ViewResult;

            // Assert
            var viewModel = (TodoItemModel)result.Model;            
            Assert.True(viewModel.ShowError);
        }

        [Fact]
        public async Task TickItem_WithoutExceptions_ShowsActiveItems()
        {            
            // Act
            var result = await _homeController.TickItem(It.IsAny<string>()) as RedirectToActionResult;

            // Assert
            Assert.Equal("ActiveItems", result.ActionName);
        }

        [Fact]
        public async Task TickItem_WithExceptions_ShowsActiveItems()
        {            
            // Assert
            const string todoItemId = "todoitemid";

            _mockTodoItemService
                .Setup(x => x.TickActiveTodoItem(todoItemId))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _homeController.TickItem(todoItemId) as RedirectToActionResult;

            // Assert
            Assert.Equal("ActiveItems", result.ActionName);
        }
    }
}