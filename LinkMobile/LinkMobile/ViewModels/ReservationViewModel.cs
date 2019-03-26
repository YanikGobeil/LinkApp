using LinkMobile.Models;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LinkMobile.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        //const 
        public const string DIRECTION_3IT = "3IT";
        public const string DIRECTION_UDES = "Université de Sherbrooke";

        //private vars
        private IMasterNavigationService _masterNavigationService;
        private IReservationService _reservationService;
        private IPageService _pageService;
        private string _direction;
        private string _userReservationTime;
        private string _userReservationDate;
        private DateTime _selectedDate;
        private DateTime _minDate;


        //public vars
        public ObservableCollection<string> TimeList { get; private set; }
        public ObservableCollection<string> DirectionsList { get; private set; }
 

        //commands
        public ICommand OnAppearingCommandForDirections { get; private set; }
        public ICommand OnAppearingCommandForMinDate { get; private set; }
        public ICommand OnDisappearingCommand { get; private set; }
        public ICommand DateSelectedCommand { get; private set; } 
        public ICommand CreateReservationCommand { get; private set; }
        public ICommand DirectionSelectedCommand { get; private set; }
        public ICommand CancelReservationCommand { get; private set; }

        public ReservationViewModel(IMasterNavigationService masterNavigationService, IReservationService reservationService, IPageService pageService)
        {
            //init publics
            TimeList = new ObservableCollection<string>();
            DirectionsList = new ObservableCollection<string>();

            //init commands
            OnAppearingCommandForDirections = new Command(SetDirectionsList);
            OnAppearingCommandForMinDate = new Command(SetMinDate);
            DateSelectedCommand = new Command<string>( async(date) => await GetAvailableHoursForDate(date));
            DirectionSelectedCommand = new Command(async () => await GetAvailableHoursForDateAndDirection());
            CreateReservationCommand = new Command(async () => await CreateReservation());
            CancelReservationCommand = new Command(CancelReservation);

            //init privates
            _masterNavigationService = masterNavigationService;
            _reservationService = reservationService;
            _pageService = pageService;
        }

        //bindings
        public string Direction
        {
            get { return _direction; }
            set { SetValue(ref _direction, value); }
        }

        public string UserReservationTime
        {
            get { return _userReservationTime; }
            set { SetValue(ref _userReservationTime, value); }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { SetValue(ref _selectedDate, value); }
        }

        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetValue(ref _minDate, value); }
        }

        public void SetDirectionsList()
        {
            DirectionsList.Clear();
            DirectionsList.Add(DIRECTION_3IT);
            DirectionsList.Add(DIRECTION_UDES);
        }

        public void SetMinDate()
        {
            MinDate = DateTime.Today;
        }

        public async Task GetAvailableHoursForDate(string date)
        {
            if (_direction != null)
            {
                _userReservationDate = date;
                var response = await _reservationService.getAvailableHours(_direction, date);
                TimeList.Clear();
                response.ForEach(time => TimeList.Add(time));
            }
            else
            {
                await _pageService.DisplayAlert("Info", "Veuillez choisir une direction.", "OK");
            }
            
        }

        public async Task GetAvailableHoursForDateAndDirection()
        {
            if (_selectedDate != null)
            {
                _userReservationDate = _selectedDate.Date.ToString();
                var response = await _reservationService.getAvailableHours(_direction, _userReservationDate);
                TimeList.Clear();
                response.ForEach(time => TimeList.Add(time));
            }
        }

        public async Task CreateReservation()
        {
            try
            {
                if (_userReservationTime != null && _userReservationDate != null && _direction != null)
                {
                    Reservation userReservation = new Reservation();
                    userReservation.Time = _userReservationTime;
                    userReservation.Date = _userReservationDate;
                    userReservation.Directions = _direction;
                    await _pageService.DisplayAlert("Confirmation", "Réservation créée avec succès!", "OK");
                }
                else
                {
                    throw new NullReferenceException("Une donnée est manquante à la complétion de la réservation.");
                }
            }
            catch (Exception e)
            {              
                await _pageService.DisplayAlert("Erreur", e.Message, "OK");
            }
           
            //TODO : userReservation.Cip = getUserDetails.... then send reservation object to server
        }

        public void CancelReservation()
        {
            //reset set variables
            Direction = null;
            _userReservationTime = null;
            _userReservationDate = null;
            SelectedDate = DateTime.Today;
            TimeList.Clear();
        }
    }
}
