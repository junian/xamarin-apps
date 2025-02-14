using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TaskApp.Models;
using TaskApp.Views;

namespace TaskApp.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        private TaskItem _selectedItem;

        public ObservableCollection<TaskItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get;  }
        public Command<TaskItem> ItemTapped { get; }
        
        public Command<TaskItem> ToggleCompleteCommand { get; }

        public TaskListViewModel()
        {
            Title = "Task Manager";
            Items = new ObservableCollection<TaskItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<TaskItem>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            ToggleCompleteCommand = new Command<TaskItem>(async (task) => await ToggleComplete(task));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetTaskListAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public TaskItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(TaskEditorPage));
        }

        private async void OnItemSelected(TaskItem item)
        {
            if (item == null)
                return;
            
            await Shell.Current.GoToAsync($"{nameof(TaskEditorPage)}?{nameof(TaskEditorViewModel.TaskId)}={item.Id}");
        }
        
        private async Task ToggleComplete(TaskItem task)
        {
            await DataStore.UpdateTaskAsync(task);
        }
    }
}
