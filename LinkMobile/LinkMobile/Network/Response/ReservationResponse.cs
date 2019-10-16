using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Network.Response
{
    public class ReservationResponse : BaseResponse
    {
        public DateTime startDateTime;

        public DateTime endDateTime;

        public string userEmail;

        public string directionName;
    }
}
