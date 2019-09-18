using LinkMobile.Models;
using LinkMobile.Network.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Network.Helpers
{
    public static class NetworkResponseConverter
    {
        public static Reservation ConvertReservationResponseToReservation(ReservationResponse response)
        {
            Reservation res = new Reservation();

            res.Cip = response.userCIP;
            res.Date = response.startDateTime.Date.ToString();
            res.Time = response.startDateTime.TimeOfDay.ToString();
            res.Directions = response.directionName;

            return res;

        }

    }
}
