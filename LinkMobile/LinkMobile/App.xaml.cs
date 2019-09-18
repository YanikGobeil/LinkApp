using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LinkMobile.Views;
using LinkMobile.ViewModels.Base;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LinkMobile
{
    public partial class App : Application
    {
        public static NavigationPage NavPage;
        public App()
        {
            InitializeComponent();
            ViewModelLocator.Initialize();
            NavPage = new NavigationPage(new HomePage());
            //MainPage = new HomePage();
               
          /*  MainPage = new MainPage
            {
                Detail = NavPage,
                Master = new MenuPage()
            };*/

            MainPage = new NavigationPage(new LoginPage())
            {
                Title = "Facebook Login"
            };

        }

        protected override void OnStart()
        {
            // Handle when your app starts.. Will have to navigate cache to see active connection
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
