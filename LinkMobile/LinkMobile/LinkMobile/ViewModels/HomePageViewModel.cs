using LinkMobile.Services.Interfaces;
using LinkMobile.ViewModels.Base;
using LinkMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LinkMobile.ViewModels
{
    public class HomePageViewModel : CancellableTaskViewModel
    {
        private IMasterNavigationService _masterNavigationService;

        public ICommand OnBackCommand { get; private set; }

        public HomePageViewModel(IMasterNavigationService masterNavigationService)
        {
 
            _masterNavigationService = masterNavigationService;
            OnBackCommand = new Command(async () => await StayOnPage());
        }

        private async Task StayOnPage()
        {
            await _masterNavigationService.NavigateToPage(new HomePage());
        }

    }
}
