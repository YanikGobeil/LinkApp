using LinkMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Static
{
    public static class StaticValues
    {
        public static FacebookProfile staticFacebookProfile { get; set; }

        public static User currentUser { get; set; }

        public static string accessToken { get; set; }
    }
}
