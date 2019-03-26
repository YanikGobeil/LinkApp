using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LinkMobile.Services.Interfaces
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task PushModalAsync(Page page);
        Task PopAsync();
        Task PopModalAsync();
        Task DisplayAlert(string title, string message, string cancel);
    }
}
