﻿using LinkMobile.Services.Interfaces;
using LinkMobile.ViewModels;
using LinkMobile.ViewModels.Base;
using LinkMobile.Views.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private HomePageViewModel _viewModel;

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
            set { Application.Current.MainPage = value; }
        }
    
        public HomePage()
        {
            InitializeComponent();

            _viewModel = ViewModelLocator.Resolve<HomePageViewModel>();
            BindingContext = _viewModel;

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;

        }

        protected override bool OnBackButtonPressed()
        {
            App.NavPage = new NavigationPage(new HomePage());
            ((MasterDetailPage)MainPage).Detail = App.NavPage;
            return true;
        }

        
        protected override void OnAppearing()
        {
           // _viewModel?.UpdateCommand?.Execute(null);
        }

        protected override void OnDisappearing()
        {
            _viewModel?.CancelRunningTaskCommand?.Execute(null);
        }

        private string ClientId = "363743677910968";


        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);

                //Content = MainStackLayout;
            }
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

                return accessToken;
            }

            return string.Empty;
        }
    }
}