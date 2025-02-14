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
        public ObservableCollection<TaskItem> TaskList { get; }
        public Command LoadTaskListCommand { get; }
        public Command AddTaskItemCommand { get;  }
        public Command<TaskItem> TaskItemTapped { get; }
        public Command<TaskItem> ToggleCompleteCommand { get; }

        public TaskListViewModel()
        {
            Title = "Task Manager";
            TaskList = new ObservableCollection<TaskItem>();
            
            LoadTaskListCommand = new Command(async () => await ExecuteLoadTaskListCommand());

            TaskItemTapped = new Command<TaskItem>(OnTaskItemSelected);

            AddTaskItemCommand = new Command(OnAddTaskItem);
            
            ToggleCompleteCommand = new Command<TaskItem>(async (task) => await ToggleComplete(task));
            
            FilteredTasks = new ObservableCollection<TaskItem>(TaskList);
            FilterTasksCommand = new Command(FilterTasks);
        }

        async Task ExecuteLoadTaskListCommand()
        {
            IsBusy = true;

            try
            {
                TaskList.Clear();
                var items = await DataStore.GetTaskListAsync(true);
                foreach (var item in items)
                {
                    TaskList.Add(item);
                }
                FilteredTasks = new ObservableCollection<TaskItem>(TaskList);
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
            SelectedTaskItem = null;
        }

        private TaskItem _selectedTaskItem;
        public TaskItem SelectedTaskItem
        {
            get => _selectedTaskItem;
            set
            {
                SetProperty(ref _selectedTaskItem, value);
                OnTaskItemSelected(value);
            }
        }

        private async void OnAddTaskItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(TaskEditorPage));
        }

        private async void OnTaskItemSelected(TaskItem item)
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
                FilteredTasks = new ObservableCollection<TaskItem>(TaskList);
            }
            else
            {
                // Filter tasks based on the search text
                var filtered = TaskList
                    .Where(t => 
                        t.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 
                        || t.Description.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
                FilteredTasks = new ObservableCollection<TaskItem>(filtered);
            }
        }
    }
}
