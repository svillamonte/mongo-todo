using System;
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
    }
}
