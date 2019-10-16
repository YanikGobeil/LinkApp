using LinkMobile.Services.Interfaces;
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
        private bool _isInit;

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
            set { Application.Current.MainPage = value; }
        }
    
        public HomePage(bool isInit)
        {
            InitializeComponent();
            _isInit = isInit;
            _viewModel = ViewModelLocator.Resolve<HomePageViewModel>();
            BindingContext = _viewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            if (!_isInit)
            {
                App.NavPage = new NavigationPage(new HomePage(true));
                ((MasterDetailPage)MainPage).Detail = App.NavPage;
            }

            return true;
        }
        
        protected override void OnAppearing()
        {
           _viewModel?.UpdateCommand?.Execute(null);
        }

    }
}