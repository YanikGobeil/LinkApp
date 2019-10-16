using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashMainPage : ContentPage
	{
		public SplashMainPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(2000);

            //await Task.WhenAll(SplashGrid.FadeTo(0, 2000),
              //  Gif.ScaleTo(10, 2000));
        }
    }
}