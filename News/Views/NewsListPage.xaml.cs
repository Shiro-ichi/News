using News.Models;
using News.Services;
using News.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsListPage : ContentPage
    {
        private APIService apiService;
        public ObservableCollection<string> Items { get; set; }
        List<NewsListModel> newslists = new List<NewsListModel>();
        public List<NewsListModel> NewsList { get { return newslists; } }

        public NewsListPage(string newscategory)
        {
            NewsListViewModel source = new NewsListViewModel(newscategory);
            BindingContext = source;
            InitializeComponent();
        }

        /* public async Task<NewsListModel> GetNewsListAsync(string newscategory)
         {
             var WebAPIUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey=7dabe40554ad42bea9cde175feed265c&category=" + newscategory; // Set your REST API URL here.
             var uri = new Uri(WebAPIUrl);
             using (var httpClient = new HttpClient())
             {
                 httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("apiKey", "7dabe40554ad42bea9cde175feed265c");
                 httpClient.DefaultRequestHeaders.Add("apiKey", "7dabe40554ad42bea9cde175feed265c");
                 httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.29.2");
                 using (var response =  await httpClient.GetAsync(uri))
                 {
                     string apiResponse = await response.Content.ReadAsStringAsync();
                     try
                     {
           *//*              var httpClient = new HttpClient();
                         var response = await httpClient.GetStringAsync(uri);
                         if (response != null)
                         {*//*
                             var News =  JsonConvert.DeserializeObject<NewsListModel>(apiResponse);
                             return News;
                     *//*    }*//*
                     }
                     catch (Exception ex)
                     {
                        await DisplayAlert("Information", ex.Message, "OK");
                     }
                 }
             }
             return null;
         }
 */
        private async void SearchBar_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            this.apiService = new APIService();            
            NewsListModel mdl = await apiService.GetNewsListAsync("", 1, 10,e.NewTextValue);
            lstView.ItemsSource = (System.Collections.IEnumerable)mdl.articles;
        }
    }
}
