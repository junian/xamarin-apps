using System.ComponentModel;
using Xamarin.Forms;
using TaskApp.ViewModels;

namespace TaskApp.Views
{
    public partial class TaskDetailPage : ContentPage
    {
        public TaskDetailPage()
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel();
        }
    }
}
