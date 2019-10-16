using LinkMobile.Services.Interfaces;
using LinkMobile.ViewModels;
using LinkMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LinkMobile.Services
{
    public class MasterNavigationService : IMasterNavigationService
    {
        private static readonly SemaphoreSlim sem = new SemaphoreSlim(1, 1);

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
            set { Application.Current.MainPage = value; }
        }

        public async Task PushAsync<T>(T page) where T : Page
        {
            await sem.WaitAsync();

            try
            {
                if (App.NavPage.CurrentPage.GetType() != typeof(T))
                {
                    await App.NavPage.PushAsync(page);
                }
                ((MasterDetailPage)MainPage).IsPresented = false;
            }
            finally
            {
                sem.Release();
            }
        }

        public async Task NavigateToPage<T>(T page) where T : Page
        {
            await sem.WaitAsync();

            try
            {
                if (App.NavPage.CurrentPage.GetType() != typeof(T))
                {
                    App.NavPage = new NavigationPage(page);
                    ((MasterDetailPage)MainPage).Detail = App.NavPage;
                }
                ((MasterDetailPage)MainPage).IsPresented = false;
            }
            finally
            {
                sem.Release();
            }
        }

        public void ResetMasterPage()
        {
            App.NavPage = new NavigationPage(new HomePage(false));

            var masterPage = new MainPage
            {
                Detail = App.NavPage,
                Master = new MenuPage()
            };

            MainPage = masterPage;
        }

        public void SetMainPage<T>(T page) where T : Page
        {
            if (MainPage.GetType() != typeof(T))
            {
                MainPage = new NavigationPage(page);
            }
        }
    }
}
