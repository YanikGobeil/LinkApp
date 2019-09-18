using LinkMobile.Models;
using LinkMobile.Network.Response;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LinkMobile.ViewModels
{
    public class MyReservationsViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private string _emptyListMessage;
        private Reservation _reservationItem;
        private Reservation _selectedReservation;

        protected UniqueExecutionTaskQueue _uniqueExecutionTaskQueue;
        protected IPageService _pageService;

        public ObservableCollection<Reservation> MyReservationsDisplay { get; set; }

        public ICommand ModifyReservationCommand;

        public MyReservationsViewModel(IPageService pageService, UniqueExecutionTaskQueue uniqueExecutionTaskQueue)
        {
            _uniqueExecutionTaskQueue = uniqueExecutionTaskQueue;
            _pageService = pageService;

            MyReservationsDisplay = new ObservableCollection<Reservation>();
            ModifyReservationCommand = new Command<Reservation>(async n => await ModifyReservation(n));
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        public string EmptyListMessage
        {
            get { return _emptyListMessage; }
            set { SetValue(ref _emptyListMessage, value); }
        }

        public Reservation ReservationItem
        {
            get { return _reservationItem; }
            set { SetValue(ref _reservationItem, value); }
        }

        public Reservation SelectedReservation
        {
            get => null;
            set { SetValue(ref _selectedReservation, value); }
        }

        public async Task UpdateMyReservationsList(Func<Task<List<Reservation>>> scheduleFunc, CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                IsRefreshing = true;

                var reservs = await scheduleFunc();
                token.ThrowIfCancellationRequested();

                MyReservationsDisplay.Clear();
                reservs.ForEach(a => MyReservationsDisplay.Add(a));
                EmptyListMessage = "Aucune réservation à afficher";
            }
            catch (Exception)
            {
                EmptyListMessage = "Erreur lors du téléchargement des réservations";
            }
            finally
            {
                if (!_uniqueExecutionTaskQueue.IsPending())
                {
                    IsRefreshing = false;
                }
            }
        }

        public async Task ModifyReservation(Reservation res)
        {
            //open reservation page with selection


            //set back to null
            _selectedReservation = null;
        }

    }
}
