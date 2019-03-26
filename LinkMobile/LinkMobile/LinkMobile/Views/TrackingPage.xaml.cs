using LinkMobile.ViewModels;
using LinkMobile.ViewModels.Base;
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
	public partial class TrackingPage : ContentPage
	{
        private TrackingViewModel _viewModel;
        public TrackingPage ()
		{
			InitializeComponent ();
            _viewModel = ViewModelLocator.Resolve<TrackingViewModel>();
            BindingContext = _viewModel;           
        }

        protected override void OnAppearing()
        {
            _viewModel?.SetRouteCommand?.Execute(null);
            _viewModel?.SetPositionsCommand?.Execute(null);
        }
	}
}