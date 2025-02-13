using System.ComponentModel;
using Xamarin.Forms;
using TaskApp.ViewModels;

namespace TaskApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
