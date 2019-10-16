using LinkMobile.Models;
using LinkMobile.Services.Interfaces;
using LinkMobile.Static;
using LinkMobile.ViewModels;
using LinkMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private LoginViewModel _viewModel;

        public LoginPage ()
		{
			InitializeComponent();
            _viewModel = ViewModelLocator.Resolve<LoginViewModel>();
        }

        private async void LoginWithFacebook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacebookConfirmLoginPage());
        }

        private void LoginWithGoogle_Clicked(object sender, EventArgs e)
        {
            IGoogleSignInPagesManager _managerService = DependencyService.Get<IGoogleSignInPagesManager>();
            _managerService.DisplayGoogleSignInNativePage();
        }

        protected override void OnAppearing()
        {
            
            //check for StaticAuth
            IGoogleUsersData _userDataHandler = DependencyService.Get<IGoogleUsersData>();
            User user = _userDataHandler.GetGoogleUsersData();
            if (user != null)
            {
                //transfer static values from the native project
                StaticValues.currentUser = user;

                //stock in preferences
                _viewModel?.OnAppearingCommandStock?.Execute(null);

            }

            _viewModel?.OnAppearingCommand?.Execute(null);
        }

    }
}