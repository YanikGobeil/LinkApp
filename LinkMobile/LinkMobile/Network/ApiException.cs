using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LinkMobile.Network
{
    internal class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ApiStatus StatusCode { get; set; }

        public string Content { get; set; }
    }
}
