using LinkMobile.Services.Interfaces;
using LinkMobile.ViewModels;
using LinkMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class GoogleLoginPage : ContentPage
	{
        private GoogleViewModel _viewModel;


        public GoogleLoginPage ()
		{
			InitializeComponent ();

           _viewModel = ViewModelLocator.Resolve<GoogleViewModel>();
            BindingContext = _viewModel;

        }

        private void LoginClicked(object sender, EventArgs e)
        {
             _viewModel?.connectCommand?.Execute(null);
        }

    }
}