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
    public class LoginViewModel
    {

        public ICommand OnAppearingCommand => new Command(UpdateOnAppearing);
        public ICommand OnAppearingCommandStock => new Command(StockOnAppearing);

        private IMasterNavigationService _masterNavigationService;
        private IPersistenceService _persistenceService;

        public LoginViewModel(IMasterNavigationService masterNavigationService, IPersistenceService persistenceService)
        {
            _masterNavigationService = masterNavigationService;
            _persistenceService = persistenceService;

        }

        private void UpdateOnAppearing()
        {
            var response = _persistenceService.GetPersistenceValueWithKey("email");
            if (!response.Equals("error"))
            {

                //bypass login 
                SetMasterPage();
            }
        }

        private void StockOnAppearing()
        {
            if (StaticValues.currentUser != null)
            {
                _persistenceService.SetPersistenceValueAndKey("email", StaticValues.currentUser.email);
                _persistenceService.SetPersistenceValueAndKey("first", StaticValues.currentUser.firstName);
                _persistenceService.SetPersistenceValueAndKey("last", StaticValues.currentUser.lastName);
                _persistenceService.SetPersistenceValueAndKey("id", StaticValues.currentUser.userId.ToString());
            }           
        }

        private void SetMasterPage()
        {
            App.NavPage = new NavigationPage(new HomePage(false));

            var masterPage = new MainPage
            {
                Detail = App.NavPage,
                Master = new MenuPage()
            };

            Application.Current.MainPage = masterPage;
        }


    }
}
