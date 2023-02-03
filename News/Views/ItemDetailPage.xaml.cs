using News.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace News.Views
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