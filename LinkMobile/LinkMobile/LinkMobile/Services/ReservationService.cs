using LinkMobile.Models;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinkMobile.Services
{
    public class ReservationService : IReservationService
    {
        public async Task<List<string>> getAvailableHours(string directions, string date)
        {
 
            List<string> hourList = new List<string>()
            {
                "09:00",
                "11:00",
                "13:00",
                "15:00",
                "17:00"
            };
           

            return hourList;
        }

        public async Task<Reservation> getUserActiveReservation(string cip)
        {
            Reservation reservation = new Reservation();

            reservation.Cip = "goby2801";
            reservation.Date = "17-02-2019";
            reservation.Directions = "3IT";
            reservation.Time = "09:00";

            return reservation;
        }
    }
}