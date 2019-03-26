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
	public partial class ReservationPage : ContentPage
	{
        private ReservationViewModel _viewModel;
		public ReservationPage ()
		{
			InitializeComponent ();
            _viewModel = ViewModelLocator.Resolve<ReservationViewModel>();
            BindingContext = _viewModel;
        }
	}
}