using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LinkMobile.Services
{
    public class PageService : IPageService
    {
        private Page MainPage => Application.Current.MainPage;

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        public async Task PushModalAsync(Page page)
        {
            await MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PopAsync()
        {
            await MainPage.Navigation.PopAsync();
        }

        public async Task PopModalAsync()
        {
            await MainPage.Navigation.PopModalAsync();
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
