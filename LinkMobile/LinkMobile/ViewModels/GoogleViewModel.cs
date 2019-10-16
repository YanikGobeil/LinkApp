using LinkMobile.Helpers;
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
    public class GoogleViewModel : BaseViewModel
    {
        public ICommand connectCommand { get; private set; }

        //private IAuthentificationService _authentificationService;
        private IPageService _pageService;
        private IMasterNavigationService _masterNavigationService;

        private string _emailInput;
        private string _passwordInput;
        public string EmailInput
        {
            get { return _emailInput; }
            set { SetValue(ref _emailInput, value); }
        }

        public string PasswordInput
        {
            get { return _passwordInput; }
            set { SetValue(ref _passwordInput, value); }
        }

        public GoogleViewModel(IPageService pageService, IMasterNavigationService masterNavigationService)
        {
            connectCommand = new Command(async () => await LoginToGoogle());

           // _authentificationService = authentificationService;
            _pageService = pageService;
            _masterNavigationService = masterNavigationService;
        }

        public async Task LoginToGoogle()
        {
            IAuthentificationService _authentificationService = DependencyService.Get<IAuthentificationService>();
            string Token = "";
            if (_emailInput != null && _passwordInput != null)
                if (credentialsValid())
                {
                    Token = await _authentificationService.LoginWithEmailPassword(_emailInput, _passwordInput);
                }                  
         
            if (Token != "")
            {
                await _masterNavigationService.PushAsync(new ReservationPage());
            }
            else
            {
                await _pageService.DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
            }
        }

        private bool credentialsValid()
        {
            if (_emailInput.EndsWith("@hotmail.com") || _emailInput.EndsWith("@gmail.com"))
                return true;

            return false;
        }

    }
}
