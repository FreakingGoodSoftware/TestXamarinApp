using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TestApp.Views
{
    public class MainPage : NavigationPage
    {
        public MainPage(ItemsPage page) : base(page)
        {
        }
    }
}