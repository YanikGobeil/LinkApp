using LinkMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Network.Request
{
    public class PostReservationRequest
    {
        public DateTime startDateTime;

        public DateTime endDateTime;

        public User user;

        public string directionName;

        public int reservationId;
    }

   
}
