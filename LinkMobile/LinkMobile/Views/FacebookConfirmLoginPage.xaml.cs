using LinkMobile.Services.Interfaces;
using LinkMobile.Static;
using LinkMobile.ViewModels;
using LinkMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacebookConfirmLoginPage : ContentPage
    {
        private Page MainPage
        {
            get { return Application.Current.MainPage; }
            set { Application.Current.MainPage = value; }
        }

        private WebView webView;

        public FacebookConfirmLoginPage()
        {
            InitializeComponent();

            BindingContext = new FacebookViewModel();

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
         
            };
            Content = webView;

            webView.Navigated += WebViewOnNavigated;

        }

        protected override bool OnBackButtonPressed()
        {
            App.NavPage = new NavigationPage(new FacebookConfirmLoginPage());
            ((MasterDetailPage)MainPage).Detail = App.NavPage;
            return true;
        }

        private string ClientId = "363743677910968";


        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);

                GoToHomePage();  
            }                            
        }

        private void GoToLoginPage(object sender, EventArgs eventArgs)
        {
            webView.GoBack();
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                Static.StaticValues.accessToken = accessToken;

                return accessToken;
            }

            return string.Empty;
        }

        private void GoToHomePage()
        {
            App.NavPage = new NavigationPage(new HomePage(false));
            App.Current.MainPage = new MainPage
            {
                Detail = App.NavPage,
                Master = new MenuPage()
            };
        }

    }
}
