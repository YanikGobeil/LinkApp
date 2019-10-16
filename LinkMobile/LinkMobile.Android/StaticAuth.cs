using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using LinkMobile.Droid.Models;

namespace LinkMobile.Droid
{
    public static class StaticAuth
    {
        public static FirebaseAuth auth;

        public static IList<FirebaseApp> firebaseAppList;

        public static GoogleApiClient api;

        public static bool isInitialized;

        public static User currentUser;
    }
}