using LinkMobile.Models;
using LinkMobile.Network.Request;
using LinkMobile.Network.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkMobile.Services.Interfaces
{
    public interface IReservationService
    {
        List<string> GetAllHours(string directions);

        Task<List<ReservationResponse>> GetReservationsForDateAndDirections(string path, string directions, DateTime selectedDate, CancellationToken token);

        Task<List<Reservation>> GetUserActiveReservations(string path, string cip, CancellationToken token);

        Task<BaseResponse> CreateReservation(string path, PostReservationRequest Treq, CancellationToken token);
    }
}