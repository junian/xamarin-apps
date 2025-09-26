using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddTaskAsync(T item);
        Task<bool> UpdateTaskAsync(T item);
        Task<bool> DeleteTaskAsync(string id);
        Task<T> GetTaskAsync(string id);
        Task<IEnumerable<T>> GetTaskListAsync(bool forceRefresh = false);
    }
}

