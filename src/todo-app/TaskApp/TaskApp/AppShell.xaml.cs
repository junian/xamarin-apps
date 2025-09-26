using System;
using System.Collections.Generic;
using TaskApp.ViewModels;
using TaskApp.Views;
using Xamarin.Forms;

namespace TaskApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TaskEditorPage), typeof(TaskEditorPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TaskListPage");
        }
    }
}

