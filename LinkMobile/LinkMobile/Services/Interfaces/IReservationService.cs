using LinkMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Services.Interfaces
{
    public interface IReservationService
    {
        List<string> getAvailableHours(string directions);

        Reservation getUserActiveReservation(string cip);
    }
}