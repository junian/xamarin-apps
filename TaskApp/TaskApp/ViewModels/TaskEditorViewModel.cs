using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TaskApp.Models;
using Xamarin.Forms;

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
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_taskTitle);
        }

        private bool _isEditMode = false;
        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        private string _taskId;
        public string TaskId
        {
            get => _taskId;
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

        private DateTime? _dueDate = DateTime.Today;
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

        private async void LoadTaskAsync(string taskId)
        {
            try
            {
                Debug.WriteLine(taskId);
                var item = await DataStore.GetTaskAsync(taskId);
                TaskTitle = item.Title;
                TaskDescription = item.Description;
                TaskDueDate = item.DueDate;
                TaskIsCompleted = item.IsCompleted;
                IsEditMode = !string.IsNullOrWhiteSpace(TaskId);
                Title = "Edit Task";
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var taskItem = new TaskItem()
            {
                Id = IsEditMode ? TaskId : Guid.NewGuid().ToString(),
                Title = TaskTitle,
                Description = TaskDescription,
                DueDate = TaskDueDate,
                IsCompleted = TaskIsCompleted,
            };

            if (IsEditMode)
                await DataStore.UpdateTaskAsync(taskItem);
            else
                await DataStore.AddTaskAsync(taskItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDelete()
        {
            bool answer = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                "Confirmation", // Title
                "Are you sure you want to delete this task?", // Message
                "Yes", // Accept button text
                "No" // Cancel button text
            );

            if (!answer)
                return;

            await DataStore.DeleteTaskAsync(TaskId);
            await Shell.Current.GoToAsync("..");
        }
    }
}

