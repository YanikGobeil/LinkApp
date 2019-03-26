using LinkMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinkMobile.Services.Interfaces
{
    public interface IReservationService
    {
        Task<List<string>> getAvailableHours(string directions, string date);

        Task<Reservation> getUserActiveReservation(string cip);
    }
}