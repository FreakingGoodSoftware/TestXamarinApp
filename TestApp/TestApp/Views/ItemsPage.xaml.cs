using System;

using TestApp.Models;
using TestApp.ViewModels;

using Xamarin.Forms;

namespace TestApp.Views
{
    public partial class ItemsPage : ContentPage
    {
        //ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            ItemsListView.ItemAppearing += ItemsListView_ItemAppearing;
        }

        private void ItemsListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            (BindingContext as ItemsViewModel).LoadMoreItems(e.Item as Item);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
}
