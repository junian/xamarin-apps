using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using TaskApp.Models;

namespace TaskApp.Services
{
	public class SQLiteDataStore : IDataStore<TaskItem>
    {
        private const string DbName = "TaskApp.db";
        private readonly SQLiteAsyncConnection _database;

        public SQLiteDataStore()
        {
            // Initialize the SQLite connection
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);
            _database = new SQLiteAsyncConnection(dbPath);

            // Create the TaskItem table if it doesn't exist
            _database.CreateTableAsync<TaskItem>().Wait();
        }

        public async Task<bool> AddTaskAsync(TaskItem item)
        {
            try
            {
                // Insert the task item into the database
                await _database.InsertAsync(item);
                return true;
            }
            catch (Exception)
            {
                // Handle exceptions (e.g., logging)
                return false;
            }
        }

        public async Task<bool> UpdateTaskAsync(TaskItem item)
        {
            try
            {
                // Update the task item in the database
                await _database.UpdateAsync(item);
                return true;
            }
            catch (Exception)
            {
                // Handle exceptions (e.g., logging)
                return false;
            }
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            try
            {
                // Find the task item by ID and delete it
                var taskItem = await _database.Table<TaskItem>().FirstOrDefaultAsync(t => t.Id.ToString() == id);
                if (taskItem != null)
                {
                    await _database.DeleteAsync(taskItem);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                // Handle exceptions (e.g., logging)
                return false;
            }
        }

        public async Task<TaskItem> GetTaskAsync(string id)
        {
            try
            {
                // Find the task item by ID
                return await _database.Table<TaskItem>().FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception)
            {
                // Handle exceptions (e.g., logging)
                return null;
            }
        }

        public async Task<IEnumerable<TaskItem>> GetTaskListAsync(bool forceRefresh = false)
        {
            try
            {
                // Retrieve all task items from the database
                return await _database.Table<TaskItem>().ToListAsync();
            }
            catch (Exception)
            {
                // Handle exceptions (e.g., logging)
                return new List<TaskItem>();
            }
        }
    }
}

