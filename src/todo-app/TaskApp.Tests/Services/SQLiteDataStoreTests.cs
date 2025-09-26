
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using TaskApp.Models;
using TaskApp.Services;

namespace TaskApp.Tests.Services
{
    [TestClass]
    public class SQLiteDataStoreTests
    {
        private SQLiteDataStore _dataStore;
        private string _dbPath;

        [TestInitialize]
        public void Initialize()
        {
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Guid.NewGuid().ToString()}.db");
            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }
            _dataStore = new SQLiteDataStore(_dbPath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }
        }

        [TestMethod]
        public async Task AddTaskAsync_ShouldAddTask()
        {
            // Arrange
            var task = new TaskItem { Id = "1", Title = "Test Task", Description = "Test Description", DueDate = DateTime.Now, IsCompleted = false };

            // Act
            var result = await _dataStore.AddTaskAsync(task);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateTaskAsync_ShouldUpdateTask()
        {
            // Arrange
            var task = new TaskItem { Id = "1", Title = "Test Task", Description = "Test Description", DueDate = DateTime.Now, IsCompleted = false };
            await _dataStore.AddTaskAsync(task);
            task.Title = "Updated Task";

            // Act
            var result = await _dataStore.UpdateTaskAsync(task);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteTaskAsync_ShouldDeleteTask()
        {
            // Arrange
            var task = new TaskItem { Id = "1", Title = "Test Task", Description = "Test Description", DueDate = DateTime.Now, IsCompleted = false };
            await _dataStore.AddTaskAsync(task);

            // Act
            var result = await _dataStore.DeleteTaskAsync("1");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetTaskAsync_ShouldReturnTask()
        {
            // Arrange
            var task = new TaskItem { Id = "1", Title = "Test Task", Description = "Test Description", DueDate = DateTime.Now, IsCompleted = false };
            await _dataStore.AddTaskAsync(task);

            // Act
            var result = await _dataStore.GetTaskAsync("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Id);
        }

        [TestMethod]
        public async Task GetTaskListAsync_ShouldReturnTaskList()
        {
            // Arrange
            var task1 = new TaskItem { Id = Guid.NewGuid().ToString(), Title = "Test Task 1", Description = "Test Description 1", DueDate = DateTime.Now, IsCompleted = false };
            var task2 = new TaskItem { Id = Guid.NewGuid().ToString(), Title = "Test Task 2", Description = "Test Description 2", DueDate = DateTime.Now, IsCompleted = true };
            await _dataStore.AddTaskAsync(task1);
            await _dataStore.AddTaskAsync(task2);

            // Act
            var result = await _dataStore.GetTaskListAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task AddTaskAsync_ShouldHandleException()
        {
            // Arrange
            var invalidTask = new TaskItem { Id = null, Title = "Test Task", Description = "Test Description", DueDate = DateTime.Now, IsCompleted = false };

            // Act
            var result = await _dataStore.AddTaskAsync(invalidTask);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UpdateTaskAsync_ShouldHandleException()
        {
            // Arrange
            var invalidTask = new TaskItem { Id = null, Title = "Test Task", Description = "Test Description", DueDate = DateTime.Now, IsCompleted = false };

            // Act
            var result = await _dataStore.UpdateTaskAsync(invalidTask);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task DeleteTaskAsync_ShouldHandleException()
        {
            // Arrange
            var invalidId = "invalid-id";

            // Act
            var result = await _dataStore.DeleteTaskAsync(invalidId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetTaskAsync_ShouldHandleException()
        {
            // Arrange
            var invalidId = "invalid-id";

            // Act
            var result = await _dataStore.GetTaskAsync(invalidId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetTaskListAsync_ShouldHandleException()
        {
            // Arrange
            // Simulate an exception by deleting the database file
            File.Delete(_dbPath);

            // Act
            var result = await _dataStore.GetTaskListAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
    }
}
