using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskApp.Services;
using TaskApp.Views;

namespace TaskApp
{
    public partial class App : Application
    {

        public App ()
        {
            InitializeComponent();

            DependencyService.Register<SQLiteDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

