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

        protected override void OnAppearing()
        {
            _viewModel?.OnAppearingCommandForDirections?.Execute(null);
            _viewModel?.OnAppearingCommandForMinDate?.Execute(null);
        }

        protected override void OnDisappearing()
        {
            _viewModel?.OnDisappearingCommand?.Execute(null);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _viewModel?.CreateReservationCommand?.Execute(null);
        }


        private void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            _viewModel?.CancelReservationCommand?.Execute(null);
        }

        private void Picker_Focused(object sender, FocusEventArgs e)
        {
           _viewModel?.OnHoursFocused?.Execute(null);
        }

        private void Direction_Changed(object sender, EventArgs e)
        {
            _viewModel?.DirectionChangedCommand?.Execute(null);
        }

        private void Date_Selected(object sender, EventArgs e)
        {
            _viewModel?.DateSelectedCommand?.Execute(null);
        }

        private void Time_Selected_Changed(object sender, EventArgs e)
        {
            _viewModel?.HoursSelectedCommand?.Execute(null);
        }
    }


}

