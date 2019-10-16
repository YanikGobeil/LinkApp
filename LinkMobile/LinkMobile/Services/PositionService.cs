using LinkMobile.Models;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LinkMobile.Services
{
    public class PositionService : IPositionService
    {

        public async Task<Position> getCurrentPosition()
        {
            
            Position currentpos = new Position();

            currentpos.Latitude = 45.379655;
            currentpos.Longitude = -71.927801;

            return currentpos;         
        }

        public async Task<List<Xamarin.Forms.Maps.Position>> getRouteCoordinates()
        {
            List<Xamarin.Forms.Maps.Position> coordsList = new List<Xamarin.Forms.Maps.Position>();
            coordsList.Add(new Xamarin.Forms.Maps.Position(45.384682, -71.928413));
            coordsList.Add(new Xamarin.Forms.Maps.Position(45.384471, -71.928435));
            coordsList.Add(new Xamarin.Forms.Maps.Position(45.384305, -71.928413));
            coordsList.Add(new Xamarin.Forms.Maps.Position(45.384350, -71.928424));
            coordsList.Add(new Xamarin.Forms.Maps.Position(45.384267, -71.928434));
            coordsList.Add(new Xamarin.Forms.Maps.Position(45.384162, -71.928456));           
            coordsList.Add(new Xamarin.Forms.Maps.Position(45.384026, -71.928445));
            coordsList.Add(new Xamarin.Forms.Maps.Position(45.383913, -71.928434));

            return coordsList;

        }
    }
}
