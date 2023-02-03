using News.Models;
using News.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Extended;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace News.ViewModels
{
    class ArticleViewModel
    {
        public Command<articleModel> ItemTapped { get; }
        public ArticleViewModel()
        {           
            ItemTapped = new Command<articleModel>(OnItemSelected);           
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
