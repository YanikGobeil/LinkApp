using LinkMobile.Models;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace LinkMobile.ViewModels
{
    public class TrackingViewModel : BaseViewModel
    {
        //private vars 
        private IMasterNavigationService _navigationService;
        private IPageService _pageService;
        private IPositionService _positionService;
        private ObservableCollection<Pin> _vehiculePositionPins = new ObservableCollection<Pin>();
        private Xamarin.Forms.Maps.Position _userPosition = new Xamarin.Forms.Maps.Position();
        private ObservableCollection<Xamarin.Forms.Maps.Position> _routeCoordinates = new ObservableCollection<Xamarin.Forms.Maps.Position>();
        private double[] coordinatesX = { 45.377634, 45.377700, 45.37770, 45.377697, 45.377691, 45.377683, 45.377738, 45.378056 };
        private double[] coordinatesY = { -71.937222, -71.938017, -71.938774, -71.939107, -71.939367, -71.939761, -71.940244, -71.940735 };
        private static Timer timer;
        private int counter;
        //public vars


        //commands
        public ICommand SetPositionsCommand { get; private set; }
        public ICommand SetScriptCommand { get; private set; }

        public TrackingViewModel(IMasterNavigationService masterNavigationService, IPageService pageService, IPositionService positionService)
        {
            //init display public vars
            UserPosition = new Xamarin.Forms.Maps.Position();
            counter = 0;
            //SetHighlightedRoute();

            //for DEMO, init pin at first position
            VehiculePositionPins.Add(new Pin() { Position = new Xamarin.Forms.Maps.Position(coordinatesX[0], coordinatesY[0]), Type = PinType.Generic, Label = "Navette autonome" });
            UserPosition = new Xamarin.Forms.Maps.Position(coordinatesX[3], coordinatesY[3]);

            //init private vars   
            _navigationService = masterNavigationService;
            _pageService = pageService;
            _positionService = positionService;
        
            

            //SetPositionsCommand = new Command(async () => await SetPositions());
            timer = new System.Timers.Timer();
            timer.Interval = 2000;

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        //bindings on property changed
        public ObservableCollection<Pin> VehiculePositionPins { get { return _vehiculePositionPins; } set { _vehiculePositionPins = value; OnPropertyChanged(); } }
        public Xamarin.Forms.Maps.Position UserPosition { get { return _userPosition; } set { _userPosition = value; OnPropertyChanged(); } }
        public ObservableCollection<Xamarin.Forms.Maps.Position> RouteCoordinates { get { return _routeCoordinates; } set { _routeCoordinates = value; OnPropertyChanged(); } }


        //methods
        private async Task SetPositions()
        {
            if (VehiculePositionPins != null)
            {
                if (VehiculePositionPins.Count > 0)
                {
                    VehiculePositionPins.Clear();
                }
                var response = await _positionService.getCurrentPosition();
                VehiculePositionPins.Add(new Pin() { Position = new Xamarin.Forms.Maps.Position(response.Latitude, response.Longitude), Type = PinType.Generic, Label = "Navette autonome"});
                UserPosition = new Xamarin.Forms.Maps.Position(response.Latitude, response.Longitude);
                
            }        

         }

       private void SetHighlightedRoute()
       {
            /*
            var response = await _positionService.getRouteCoordinates();
            response.ForEach(coordinate => RouteCoordinates.Add(coordinate));
            */
            for (int j = 0; j < coordinatesX.Length; j++)
            {
                RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(coordinatesX[j],coordinatesY[j]));
            }
       }    

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (VehiculePositionPins.Count > 0)
            {
                VehiculePositionPins.Clear();
            }
            VehiculePositionPins.Add(new Pin() { Position = new Xamarin.Forms.Maps.Position(coordinatesX[counter], coordinatesY[counter]), Type = PinType.Generic, Label = "Navette autonome" });
            UserPosition = new Xamarin.Forms.Maps.Position(coordinatesX[3], coordinatesY[3]);
            if (counter < coordinatesX.Length-1)
            {
                counter++;
            }
            else
            {
                timer.Stop();
                _pageService.DisplayAlert("test", "timer stopped", "OK");
            }
                
        }
    }
}
