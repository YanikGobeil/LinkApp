using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LinkMobile.Services.Interfaces
{
    public interface IMasterNavigationService
    {
        Task PushAsync<T>(T page) where T : Page;

        Task NavigateToPage<T>(T page) where T : Page;

        void ResetMasterPage();

        void SetMainPage<T>(T page) where T : Page;
    }
}
