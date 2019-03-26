using LinkMobile.Services.Interfaces;
using LinkMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LinkMobile.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private IMasterNavigationService _masterNavigationService;

        public ICommand OpenHomeCommand => new Command(async () => await OpenHome());

        public ICommand OpenScheduleCommand => new Command(async () => await OpenSchedule());

        public ICommand OpenReservationCommand => new Command(async () => await OpenReservation());

        public ICommand OpenMyReservationsCommand => new Command(async () => await OpenMyReservations());

        public ICommand OpenNewConnectionCommand => new Command(OpenNewConnection);

        public ICommand OpenLocalisationCommand => new Command(async () => await OpenLocalisation());

        public MenuPageViewModel(IMasterNavigationService masterNavigationService)
        {
            _masterNavigationService = masterNavigationService;
        }

        private async Task OpenHome()
        {
            await _masterNavigationService.NavigateToPage(new HomePage());
        }

        private async Task OpenSchedule()
        {
            //await _masterNavigationService.NavigateToPage(new SchedulePage());
        }

        private async Task OpenReservation()
        {
            await _masterNavigationService.PushAsync(new ReservationPage());
        }

        private async Task OpenMyReservations()
        {
            //await _masterNavigationService.NavigateToPage(new MyReservationsPage());
        }

        private void OpenNewConnection()
        {
            //_networkService.SetToken(string.Empty);
            //_masterNavigationService.SetMainPage(new AuthentificationPage());
        }

        private async Task OpenLocalisation()
        {
            await _masterNavigationService.PushAsync(new TrackingPage());
        }

    }
}
