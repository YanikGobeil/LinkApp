using LinkMobile.Models;
using LinkMobile.Network.Request;
using LinkMobile.Network.Response;
using LinkMobile.Services.Interfaces;
using LinkMobile.Services.Interfaces.Persistence;
using LinkMobile.Static;
using LinkMobile.ViewModels.Base;
using LinkMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LinkMobile.ViewModels
{
    public class ReservationViewModel : CancellableTaskViewModel
    {
        //const 
        public const string DIRECTION_3IT = "3IT";
        public const string DIRECTION_UDES = "Université de Sherbrooke";
        public const string path = "/reservations";
        public const string pathDateAndDirections = "reservations/byDate/";

        //private vars
        private IMasterNavigationService _masterNavigationService;
        private IReservationService _reservationService;
        private IPersistenceService _persistenceService;
        private IPageService _pageService;

        private string _direction;
        private string _userReservationTime;
        private string _userReservationDate;
        private DateTime _selectedDate;
        private DateTime _minDate;
        private Reservation _userReservation;


        //public vars
        public ObservableCollection<string> TimeList { get; private set; }
        public ObservableCollection<string> DirectionsList { get; private set; }
 

        //commands
        public ICommand OnAppearingCommandForDirections { get; private set; }
        public ICommand OnAppearingCommandForMinDate { get; private set; }
        public ICommand OnDisappearingCommand { get; private set; }
        public ICommand OnHoursFocused { get; private set; }
        public ICommand CreateReservationCommand { get; private set; }
        public ICommand CancelReservationCommand { get; private set; }

        public ICommand DirectionChangedCommand { get; private set; }

        public ICommand DateSelectedCommand { get; private set; }

        public ReservationViewModel(IMasterNavigationService masterNavigationService, IReservationService reservationService, IPageService pageService, IPersistenceService persistenceService)
        {
            //init publics
            TimeList = new ObservableCollection<string>();
            DirectionsList = new ObservableCollection<string>();

            //init commands
            OnAppearingCommandForDirections = new Command(SetDirectionsList);
            OnAppearingCommandForMinDate = new Command(SetMinDate);
            CreateReservationCommand = new Command(async () => await CreateReservation());
            CancelReservationCommand = new Command(CancelReservation);
            DirectionChangedCommand = new Command(async () => await GetAvailableHoursForDateAndDirection());
            DateSelectedCommand = new Command(async () => await GetAvailableHoursForDateAndDirection());

            //init privates
            _masterNavigationService = masterNavigationService;
            _reservationService = reservationService;
            _pageService = pageService;
            _persistenceService = persistenceService;
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

        private async Task GetAvailableHoursForDateAndDirection()
        {
            if (SelectedDate != null && Direction != null && Direction != "")
            {
                CancellationToken token = new CancellationToken();
                List<ReservationResponse> reservations = await _reservationService.GetReservationsForDateAndDirections(pathDateAndDirections, Direction, SelectedDate, token);
                _userReservationDate = _selectedDate.Date.ToString();
                List<string> allHoursList = _reservationService.GetAllHours(_direction);
                DateTime newSelectedDate = new DateTime();
                UseTempDateTime(newSelectedDate);
                ParseHourArrays(allHoursList);
                RemoveReservedHours(allHoursList, reservations);
                TimeList.Clear();
                allHoursList.ForEach(time => TimeList.Add(time));
            }
             
        }

        public async Task CreateReservation()
        {          
            try
            {
                if (_userReservationTime != null && _userReservationDate != null && _direction != null)
                {
                    //set userReservation private variable
                    _userReservation = new Reservation();
                    _userReservation.Time = _userReservationTime;
                    _userReservation.Date = _userReservationDate;
                    _userReservation.Email = _persistenceService.GetPersistenceValueWithKey("email").ToString();
                    _userReservation.Directions = _direction;

                    //cast userReservation object to Treq object
                    PostReservationRequest Treq = new PostReservationRequest();
                    CastReservationToPostRequestReservation(Treq);

                    //call API
                    var token = new CancellationToken();
                    var response = await _reservationService.CreateReservation(path, Treq, token);
                    await _pageService.DisplayAlert("Confirmation", "Réservation créée avec succès!", "OK");
                    await _masterNavigationService.NavigateToPage(new HomePage(false));
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

            //reset variables
            CancelReservation();
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

        public void CastReservationToPostRequestReservation(PostReservationRequest Treq)      
        {
            Treq.user = StaticValues.currentUser;

            if (_userReservation.Directions == DIRECTION_UDES)
            {
                Treq.directionName = "UdeS";
            }
            else
            {
                Treq.directionName = _userReservation.Directions;
            }
            
            Treq.endDateTime = new DateTime();

            //cast string date to Date
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(_userReservation.Date);

            string[] reservationTimeSplit = _userReservationTime.Split(':');          
            int hours = Int32.Parse(reservationTimeSplit[0]);
            int minutes = Int32.Parse(reservationTimeSplit[1]);

            dt = dt.Date.AddHours(hours).AddMinutes(minutes);

            Treq.startDateTime = dt;
        }

        private void ParseHourArrays(List<string> hours)
        {
            if (_selectedDate != null)
            {
                if (_selectedDate.Date == DateTime.Now.Date)
                {
                    for (int k = 0; k < hours.Count; k++)
                    {
                        string[] splits = hours[k].Split(':');

                        if (Int32.Parse(splits[0]) < DateTime.Now.Hour)
                        {
                            hours.RemoveAt(k);
                            k = k - 1;
                        }
                        else if (Int32.Parse(splits[0]) == DateTime.Now.Hour && Int32.Parse(splits[1]) <= DateTime.Now.Minute)
                        {
                            hours.RemoveAt(k);
                            k = k - 1;
                        }
                        else
                        {
                            //valid
                        }
                    }
                }
            }
            
        }

        private void RemoveReservedHours(List<string> hours, List<ReservationResponse> reservations)
        {
            //var reservs = await reservations;
            for (int i = 0; i < reservations.Count; i++)
            {
                for (int j = 0; j < hours.Count; j++)
                {
                    if ((reservations[i].startDateTime.Hour.ToString() + ":" + reservations[i].startDateTime.Minute.ToString()) == hours[j])
                    {
                        hours.RemoveAt(j);
                        j = j - 1;
                    }

                }

            }
        }

        private void UseTempDateTime(DateTime dt)
        {
            DateTime now = DateTime.Now;
            dt = _selectedDate;
            dt = dt.AddHours(now.Hour);
            dt = dt.AddMinutes(now.Minute);           
        }

    }
}
