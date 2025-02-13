using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaskApp.Models;
using Xamarin.Forms;

namespace TaskApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public NewItemViewModel()
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var newItem = new TaskItem()
            {
                Id = Guid.NewGuid().ToString(),
                Title = TaskTitle,
                Description = TaskDescription,
                DueDate = TaskDueDate,
                IsCompleted = TaskIsCompleted,
            };
            
            await DataStore.AddTaskAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

