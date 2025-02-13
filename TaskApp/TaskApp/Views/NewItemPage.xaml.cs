using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TaskApp.Models;
using TaskApp.ViewModels;

namespace TaskApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public TaskItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}
