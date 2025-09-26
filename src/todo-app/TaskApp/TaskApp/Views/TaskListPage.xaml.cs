using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TaskApp.Models;
using TaskApp.Views;
using TaskApp.ViewModels;

namespace TaskApp.Views
{
    public partial class TaskListPage : ContentPage
    {
        TaskListViewModel _viewModel;

        public TaskListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TaskListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
