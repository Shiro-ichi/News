using News.Models;
using News.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowsePage : ContentPage
    {
        NewsCategoriesViewModel _viewModel;
        ObservableCollection<Item> categories;
        public BrowsePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewsCategoriesViewModel();
            categories = new ObservableCollection<Item>                            
            {   new Item {Id = Guid.NewGuid().ToString(),Text="business", Description="Business"},
                new Item {Id = Guid.NewGuid().ToString(),Text="entertainment", Description="Entertainment"},
                new Item {Id = Guid.NewGuid().ToString(),Text="general", Description="General"},
                new Item {Id = Guid.NewGuid().ToString(),Text="health", Description="Health"},
                new Item {Id = Guid.NewGuid().ToString(),Text="science", Description="Science"},
                new Item {Id = Guid.NewGuid().ToString(),Text="sports", Description="Sports"},
                new Item {Id = Guid.NewGuid().ToString(),Text="technology", Description="Technology"}
            };
            NewsCategory.ItemsSource = categories;
        }
        
        public async Task<List<NewsCategoryModel>> GetCategories()
        {
            var WebAPIUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey=7dabe40554ad42bea9cde175feed265c"; // Set your REST API URL here.
            var uri = new Uri(WebAPIUrl);
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var categories = JsonConvert.DeserializeObject<List<NewsCategoryModel>>(response.ToString());
                    return categories;
                }
            }
            catch (Exception ex)
            {
                ResponseModel obj = new ResponseModel();
                obj.IsSuccess = false;
                obj.Message = ex.Message;
                return null;
            }
            return null;
        }

        private void Categories_Refreshing(object sender, EventArgs e)
        {

        }


        private async void Categories_Clicked(object sender ,EventArgs e)
        {
            var lbl = sender as Label;

            //var selectedItem = lbl.BindingContext as News.Models.Item;
            //if (selectedItem == null) return;
            //var parameter = sender as News.Models.Item;
            // var mi = ((MenuItem)sender);
            //News.Models.Item mdl = (News.Models.Item)mi.CommandParameter;

            await Navigation.PushAsync(new NewsListPage(lbl.Text));
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewsCategory.ItemsSource = categories.Where(s => s.Text.StartsWith(e.NewTextValue));            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}