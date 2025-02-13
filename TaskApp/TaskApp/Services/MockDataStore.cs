using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Models;

namespace TaskApp.Services
{
    public class MockDataStore : IDataStore<TaskItem>
    {
        readonly List<TaskItem> items;

        public MockDataStore()
        {
            items = new List<TaskItem>()
            {
                new TaskItem { Id = Guid.NewGuid().ToString(), Title = "1st Task", Description="This is an item description.", DueDate = null, IsCompleted = false },
                new TaskItem { Id = Guid.NewGuid().ToString(), Title = "2nd Task", Description="This is an item description.", DueDate = DateTime.Now },
                new TaskItem { Id = Guid.NewGuid().ToString(), Title = "3rd Task", Description="This is an item description." },
                new TaskItem { Id = Guid.NewGuid().ToString(), Title = "4th Task", Description="This is an item description." },
                new TaskItem { Id = Guid.NewGuid().ToString(), Title = "5th Task", Description="This is an item description." },
                new TaskItem { Id = Guid.NewGuid().ToString(), Title = "6th Task", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(TaskItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(TaskItem item)
        {
            var oldItem = items.Where((TaskItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((TaskItem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TaskItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TaskItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
