using LinkMobile.Models;
using LinkMobile.Services;
using LinkMobile.Services.Interfaces;
using LinkMobile.Services.Interfaces.Persistence;
using LinkMobile.Static;
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
        private IPersistenceService _persistenceService;
        private FacebookService facebookService;

        public ICommand OpenHomeCommand => new Command(async () => await OpenHome());

        public ICommand OpenScheduleCommand => new Command(async () => await OpenSchedule());

        public ICommand OpenReservationCommand => new Command(async () => await OpenReservation());

        public ICommand OpenMyReservationsCommand => new Command(async () => await OpenMyReservations());

        public ICommand OpenNewConnectionCommand => new Command(OpenNewConnection);

        public ICommand OpenLocalisationCommand => new Command(async () => await OpenLocalisation());

        public MenuPageViewModel(IMasterNavigationService masterNavigationService, IPersistenceService persistenceService)
        {
            _masterNavigationService = masterNavigationService;
            _persistenceService = persistenceService;
            facebookService = new FacebookService();
        }

        private async Task OpenHome()
        {
            await _masterNavigationService.NavigateToPage(new HomePage(false));
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

        private async void OpenNewConnection()
        {
            //clear fb data if connected by fb
            if (StaticValues.accessToken != null && StaticValues.staticFacebookProfile != null)
            {

                await facebookService.Logout(StaticValues.accessToken, StaticValues.staticFacebookProfile.Id);

                StaticValues.staticFacebookProfile = null;
                StaticValues.accessToken = null;

            }

            //clear google data if connected by google
            IGoogleUsersData _userDataHandler = DependencyService.Get<IGoogleUsersData>();
            User user = _userDataHandler.GetGoogleUsersData();
            if (user != null)
            {
                _userDataHandler.ClearGoogleUserData();
                StaticValues.currentUser = null;
            }
                   
            //clear the persistence
            _persistenceService.ClearPreferences();
            
            //clear webviews cache
            DependencyService.Get<IWebCookiesPersistenceService>().RemoveCookies();

            //go to login
            _masterNavigationService.SetMainPage(new LoginPage());
        }

        private async Task OpenLocalisation()
        {
            await _masterNavigationService.PushAsync(new TrackingPage());
        }

    }
}
