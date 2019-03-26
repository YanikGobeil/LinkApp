using LinkMobile.Models;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
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
        


        //public vars


        //commands
        public ICommand SetPositionsCommand { get; private set; }
        public ICommand SetRouteCommand { get; private set; }

        public TrackingViewModel(IMasterNavigationService masterNavigationService, IPageService pageService, IPositionService positionService)
        {
            //init display public vars
            UserPosition = new Xamarin.Forms.Maps.Position();

            //init private vars   
            _navigationService = masterNavigationService;
            _pageService = pageService;
            _positionService = positionService;

            SetRouteCommand = new Command(async () => await SetHighlightedRoute());
            SetPositionsCommand = new Command(async () => await SetPositions());
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

       private async Task SetHighlightedRoute()
       {
            var response = await _positionService.getRouteCoordinates();
            response.ForEach(coordinate => RouteCoordinates.Add(coordinate));
       }    
    }
}
