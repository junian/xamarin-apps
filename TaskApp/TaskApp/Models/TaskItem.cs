using System;
using SQLite;

namespace TaskApp.Models
{
    public class TaskItem
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
