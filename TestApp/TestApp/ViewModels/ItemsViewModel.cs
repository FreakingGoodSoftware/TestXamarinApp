using Prism.Navigation;
using System;
using System.Net.Http;
using System.Threading.Tasks;

using TestApp.Helpers;
using TestApp.Models;
using TestApp.Services;

using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel, INavigationAware
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string Place { get; set; }
        string _nextPageToken = null;



        public ItemsViewModel(HttpService httpService) : base(httpService)
        {         
            Title = "Search places";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy || String.IsNullOrEmpty(Place))
                return;

            IsBusy = true;

            try
            {
                string placeEscaped = Place.Replace(' ', '+');
                var response = await HttpService.RequestAsync<string, PlacesDto>(
                    $"https://maps.googleapis.com/maps/api/place/textsearch/json?query={placeEscaped}&key=AIzaSyArMRLpN8of2plXwm_WC-GNJL3PChXyQX0&pagetoken={_nextPageToken}",
                    null,
                    HttpMethod.Get,
                    cts.Token);
                _nextPageToken = response.next_page_token;
                foreach (var item in response.results)
                {
                    var model = new Item()
                    {
                        Image = item.icon,
                        Title = item.name,
                        Address = item.formatted_address,
                        Rating = item.rating
                    };
                    Items.Add(model);
                }
            }
            catch (Exception ex) when (Log(ex)) { }
            catch(CoreException ex) when (Process(ex)) { }
            finally
            {
                IsBusy = false;
            }
        }
        internal void LoadMoreItems(Item item)
        {
            if(Items.IndexOf(item) == Items.Count - 1 && _nextPageToken != null)
            {
                LoadItemsCommand.Execute(null);
            }
        }
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (Items.Count == 0)
            {
                Items.Clear();
                LoadItemsCommand.Execute(null);
            }

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}