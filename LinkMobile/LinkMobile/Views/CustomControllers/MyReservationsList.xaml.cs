using LinkMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkMobile.Views.CustomControllers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyReservationsList : ContentView
    {
        public static readonly BindableProperty IsRefreshingProperty =
            BindableProperty.Create(nameof(IsRefreshing), typeof(bool), typeof(MyReservationsList), false, BindingMode.TwoWay);

        public static readonly BindableProperty EmptyListMessageProperty =
           BindableProperty.Create(nameof(EmptyListMessage), typeof(string), typeof(MyReservationsList));

        public readonly BindableProperty ReservationsProperty =
            BindableProperty.Create(nameof(MyReservations), typeof(ICollection<Reservation>), typeof(MyReservationsList));

        public static readonly BindableProperty ModifyReservationCommandProperty =
          BindableProperty.Create(nameof(ModifyReservationCommand), typeof(ICommand), typeof(MyReservationsList));

        public bool IsRefreshing
        {
            get { return (bool)GetValue(IsRefreshingProperty); }
            set { SetValue(IsRefreshingProperty, value); }
        }

        public string EmptyListMessage
        {
            get { return (string)GetValue(EmptyListMessageProperty); }
            set { SetValue(EmptyListMessageProperty, value); }
        }

        public ICollection<Reservation> MyReservations
        {
            get { return (ICollection<Reservation>)GetValue(ReservationsProperty); }
            set { SetValue(ReservationsProperty, value); }
        }

        public MyReservationsList()
        {
            InitializeComponent();

        }

        public ICommand ModifyReservationCommand
        {
            get { return (ICommand)GetValue(ModifyReservationCommandProperty); }
            set { SetValue(ModifyReservationCommandProperty, value); }
        }

        protected void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (ModifyReservationCommand?.CanExecute(e.SelectedItem) ?? false)
            {
                ModifyReservationCommand?.Execute(e.SelectedItem);
            }

        }
    }
}