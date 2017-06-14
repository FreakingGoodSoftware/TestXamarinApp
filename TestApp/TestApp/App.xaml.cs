using System;
using Prism.Unity;
using TestApp.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestApp.ViewModels;
using Microsoft.Practices.Unity;
using TestApp.Services;
#if !DEBUG
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#endif
namespace TestApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            InitializeComponent();

        }

        public async void SetMainPage()
        {
            MainPage = new NavigationPage();
            await NavigationService.NavigateAsync("ItemsPage");
        }

        protected override void OnInitialized()
        {
            SetMainPage();
        }

        protected override void RegisterTypes()
        {
            //Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<ItemsPage, ItemsViewModel>();
            Container.RegisterType<HttpService>(new ContainerControlledLifetimeManager());
        }
    }
}
