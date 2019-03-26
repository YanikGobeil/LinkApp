using LinkMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LinkMobile.Services.Interfaces
{
    public interface IPositionService
    {
        Task<Position> getCurrentPosition();

        Task<List<Xamarin.Forms.Maps.Position>> getRouteCoordinates();

    }
}