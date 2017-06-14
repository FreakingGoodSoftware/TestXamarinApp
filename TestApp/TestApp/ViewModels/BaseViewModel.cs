using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Threading;
using TestApp.Services;

namespace TestApp.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        protected readonly HttpService HttpService;
        protected CancellationTokenSource cts;
        public BaseViewModel(HttpService httpService)
        {
            HttpService = httpService;
            cts = new CancellationTokenSource();
        }
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool Log(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
        /// <summary>
        /// To show message, currently just to debug
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected bool Process(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}

