using System;
using System.Linq;
using Moq;
using TodoList.Models;
using TodoList.Repositories.Interfaces;
using TodoList.Services;
using TodoList.Services.Interfaces;
using Xunit;

namespace TodoList.Services.Tests
{
    public class TodoItemServiceTests
    {
        private Mock<ITodoItemRepository> _mockTodoItemRepository;
        private readonly ITodoItemService _todoItemService;

        public TodoItemServiceTests() 
        {
            _mockTodoItemRepository = new Mock<ITodoItemRepository>();
            _todoItemService = new TodoItemService(_mockTodoItemRepository.Object);
        }

        [Fact]
        public void InsertTodoItem()
        {
            // Arrange
            var todoItem = new TodoItem();

            // Act
            _todoItemService.InsertTodoItem(todoItem);

            // Assert
            _mockTodoItemRepository.Verify(x => x.InsertTodoItem(todoItem), Times.Once);
        }

        [Fact]
        public void GetActiveTodoItems()
        {
            // Arrange
            var todoItemOne = new TodoItem { Description = "One", Completed = true };
            var todoItemTwo = new TodoItem { Description = "One", Completed = false };
            var todoItemThree = new TodoItem { Description = "One", Completed = false };

            _mockTodoItemRepository
                .Setup(x => x.GetTodoItems())
                .Returns(new [] { todoItemOne, todoItemTwo, todoItemThree });

            // Act
            var result = _todoItemService.GetActiveTodoItems();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(todoItemTwo, result.ElementAt(0));
            Assert.Equal(todoItemThree, result.ElementAt(1));
        }
    }
}
