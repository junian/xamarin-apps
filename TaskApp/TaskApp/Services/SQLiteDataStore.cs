using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly string DefaultDBPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);

        public SQLiteDataStore()
            :this("")
        {
        }
        public SQLiteDataStore(string dbPath = null)
        {
            // Initialize the SQLite connection
            var selectedDBPath = string.IsNullOrWhiteSpace(dbPath)
                ? DefaultDBPath
                : dbPath;
            _database = new SQLiteAsyncConnection(selectedDBPath);

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
                var row = await _database.UpdateAsync(item);
                return row > 0;
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
                var taskItem = await _database.Table<TaskItem>().FirstOrDefaultAsync(t => t.Id == id);
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

