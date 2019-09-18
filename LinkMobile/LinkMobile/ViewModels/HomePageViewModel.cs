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
        private IReservationService _reservationService;
        private IPageService _pageService;

        public ICommand OnBackCommand { get; private set; }

        public ICommand UpdateCommand => new Command(async () => await Update());

        public MyReservationsViewModel MyReservationsViewModel { get; private set; }

        public HomePageViewModel(IMasterNavigationService masterNavigationService, IReservationService reservationService, IPageService pageService)
        {
            MyReservationsViewModel = new MyReservationsViewModel(pageService, _uniqueExecutionTaskQueue);
            _masterNavigationService = masterNavigationService;
            _reservationService = reservationService;
            _pageService = pageService;
            
            OnBackCommand = new Command(async () => await StayOnPage());
        }

        private async Task StayOnPage()
        {
            await _masterNavigationService.NavigateToPage(new HomePage());
        }

        private async Task Update()
        {
            await _uniqueExecutionTaskQueue.Execute(async (token) =>
            {              
                await MyReservationsViewModel.UpdateMyReservationsList(async () =>
                {
                    return await _reservationService.GetUserActiveReservations("", "goby2801", token);
                }
                , token);
            });
        }

    }
}
