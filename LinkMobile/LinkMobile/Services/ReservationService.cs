using LinkMobile.Models;
using LinkMobile.Network.Helpers;
using LinkMobile.Network.Request;
using LinkMobile.Network.Response;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkMobile.Services
{
    public class ReservationService : IReservationService
    {
        private INetworkService _networkService;

        public ReservationService(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public List<string> GetAllHours(string directions)
        {
            List<string> hourList;
            if (directions == "3IT")
            {
                hourList = new List<string>()
                {
                    "09:00",
                    "10:00",
                    "11:00",
                    "12:00",
                    "13:00",
                    "14:00",
                    "15:00",
                    "16:00"
                };
            }
            else
            {
                hourList = new List<string>()
                {
                    "08:30",
                    "09:30",
                    "10:30",
                    "11:30",
                    "12:30",
                    "13:30",
                    "14:30",
                    "15:30",
                    "16:30"
                };
            }
        
            return hourList;
        }

        public async Task<List<Reservation>> GetUserActiveReservations(string path, string cip, CancellationToken token)
        {
            var now = DateTime.Now;
            var response = new ReservationResponse();

            response.directionName = "3IT";
            response.endDateTime = now;
            response.startDateTime = now;
            if (Static.StaticValues.staticFacebookProfile != null)
                response.userEmail = Static.StaticValues.staticFacebookProfile.Email;
            

            List<Reservation> reservations = new List<Reservation>();
            reservations.Add(NetworkResponseConverter.ConvertReservationResponseToReservation(response));


            return reservations;
        }

        public async Task<BaseResponse> CreateReservation(string path, PostReservationRequest Treq, CancellationToken token)
        {

            var response = await _networkService.PostAsync<PostReservationRequest, BaseResponse>($"reservations/" , Treq, token);
            return response;
        }

        
        public async Task<List<ReservationResponse>> GetReservationsForDateAndDirections(string path, string directions, DateTime selectedDate, CancellationToken token)
        {
            string dateStr = selectedDate.ToString("yyyy-MM-dd HH:mm:ss");
            var response =  await _networkService.GetListAsync<ReservationResponse>($"reservations/byDate/" + dateStr + "/" + directions, token);
            return response; 
        }
    }
}