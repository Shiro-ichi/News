using News.Models;
using News.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace News.ViewModels
{
    public class NewsCategoriesViewModel : BaseViewModel
    {
        private NewsCategoryModel _selectedItem;

        public ObservableCollection<Item> Categories { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<NewsCategoryModel> ItemTapped { get; }

        public NewsCategoriesViewModel()
        {
            Title = "Browse";
            Categories = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadCategoriesCommand());

            ItemTapped = new Command<NewsCategoryModel>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = await DataStore.GetItemsAsync(true);
                foreach (var item in categories)
                {
                    Categories.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public NewsCategoryModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(NewsCategoryModel category)
        {
            if (category == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={category.Id}");
        }
    }
}