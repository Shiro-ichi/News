using News.Models;
using News.Services;
using News.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace News.ViewModels
{
    public class NewsListViewModel : BindableObject
    {
        private APIService apiService;
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<articleModel> TitleTapped { get; }
        public InfiniteScrollCollection<articleModel> Items { get; set; }

        public static readonly BindableProperty IsWorkingProperty =
            BindableProperty.Create(nameof(IsWorking), typeof(bool), typeof(NewsListViewModel), default(bool));
        public bool IsWorking
        {
            get { return (bool)GetValue(IsWorkingProperty); }
            set { SetValue(IsWorkingProperty, value); }
        }

        private const int PageSize = 10;

        /*        public ObservableCollection<NewsListModel> NewsListModel
                {
                    get { return this.NewsListModel; }
                    set { this.NewsListModel = value; }
                }*/        

        public NewsListViewModel(string category)
        {
            this.apiService = new APIService();
            LoadItemsCommand = new Command(async () => await apiService.GetNewsListAsync(category,1,PageSize));
            TitleTapped = new Command<articleModel>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            
            Items = new InfiniteScrollCollection<articleModel>
            {
                OnLoadMore = async () =>
                {
                    IsWorking = true;
                    // load the next page
                    var page = Items.Count / PageSize;
                    NewsListModel source = await apiService.GetNewsListAsync(category,page+1,PageSize);
                    //var items =  await GetItemsAsync(source,page + 1, PageSize);
                    IEnumerable<articleModel> article = source.articles;
                    IsWorking = false;
                    return article;
                  
                }
            };
           
            RefreshCommand = new Command(() =>
            {
                // clear and start again
                Items.Clear();
                Items.LoadMoreAsync();
            });

            // load the initial data
            Items.LoadMoreAsync();
        }

        public ICommand RefreshCommand { get; }     

        public void OnAppearing()
        {            
            IsWorking = true;
        }
     
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(articleModel mdl)
        {
            if (mdl == null)
                return;

            // 4. Show the article detail on web view when user click one of the article.
            await Browser.OpenAsync(mdl.url, BrowserLaunchMode.SystemPreferred);
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={category.Id}");
        }
    }
}
