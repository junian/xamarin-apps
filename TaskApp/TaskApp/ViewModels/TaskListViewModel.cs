using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
            // Initialize filtered tasks with all tasks
            FilteredTasks = new ObservableCollection<TaskItem>(Items);

            // Command to filter tasks
            FilterTasksCommand = new Command(FilterTasks);
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
                FilteredTasks = new ObservableCollection<TaskItem>(Items);
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

        private ObservableCollection<TaskItem> _filteredTasks;
        public ObservableCollection<TaskItem> FilteredTasks
        {
            get => _filteredTasks;
            set => SetProperty(ref _filteredTasks, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterTasks(); // Filter tasks whenever the search text changes
            }
        }

        public ICommand FilterTasksCommand { get; }

        private void FilterTasks()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // If search text is empty, show all tasks
                FilteredTasks = new ObservableCollection<TaskItem>(Items);
            }
            else
            {
                // Filter tasks based on the search text
                var filtered = Items
                    .Where(t => 
                        t.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 
                        || t.Description.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
                FilteredTasks = new ObservableCollection<TaskItem>(filtered);
            }
        }
    }
}
