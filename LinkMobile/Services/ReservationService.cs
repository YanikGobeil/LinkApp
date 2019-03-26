using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Services
{
    public class ReservationService : IReservationService
    {
        List<string> getAvailableHours(string directions)
        {

        }

        Reservation getUserActiveReservation(string cip);
    }
}