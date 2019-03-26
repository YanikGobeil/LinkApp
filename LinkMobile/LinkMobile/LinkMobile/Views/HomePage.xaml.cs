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

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
            set { Application.Current.MainPage = value; }
        }
    
        public HomePage()
        {
            InitializeComponent();
            myImage.Source = ImageSource.FromResource("LinkMobile.Resources.logo1.png");

        }



        protected override bool OnBackButtonPressed()
        {
            App.NavPage = new NavigationPage(new HomePage());
            ((MasterDetailPage)MainPage).Detail = App.NavPage;
            return true;
        }
    }
}