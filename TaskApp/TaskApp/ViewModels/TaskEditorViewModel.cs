using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TaskApp.Models;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TaskApp.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class TaskEditorViewModel : BaseViewModel
    {
        public TaskEditorViewModel()
        {
            Title = "Add New Task";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += 
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_taskTitle)
                && !String.IsNullOrWhiteSpace(_taskDescription);
        }

        private string _taskId;
        public string TaskId
        {
            get
            {
                return _taskId;
            }
            set
            {
                _taskId = value;
                LoadTaskAsync(value);
            }
        }

        private string _taskTitle;
        public string TaskTitle
        {
            get => _taskTitle;
            set => SetProperty(ref _taskTitle, value);
        }

        private string _taskDescription;
        public string TaskDescription
        {
            get => _taskDescription;
            set => SetProperty(ref _taskDescription, value);
        }

        private DateTime? _dueDate = null;
        public DateTime? TaskDueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        private bool _isCompleted = false;
        public bool TaskIsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }

        public async void LoadTaskAsync(string taskId)
        {
            try
            {
                Debug.WriteLine(taskId);
                var item = await DataStore.GetTaskAsync(taskId);
                TaskTitle = item.Title;
                TaskDescription = item.Description;
                TaskDueDate = item.DueDate;
                TaskIsCompleted = item.IsCompleted;
                Title = "Edit Task";
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var isEditMode = !string.IsNullOrWhiteSpace(TaskId);

            var taskItem = new TaskItem()
            {
                Id = isEditMode ? TaskId : Guid.NewGuid().ToString(),
                Title = TaskTitle,
                Description = TaskDescription,
                DueDate = TaskDueDate,
                IsCompleted = TaskIsCompleted,
            };

            if (isEditMode)
                await DataStore.UpdateTaskAsync(taskItem);
            else
                await DataStore.AddTaskAsync(taskItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

