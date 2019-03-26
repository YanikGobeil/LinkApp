using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Network.Response
{
    public class BaseResponse
    {
        public ApiStatus status { get; set; }

        public ulong timestamp { get; set; }

        public string message { get; set; }

        public string details { get; set; }
    }
}
