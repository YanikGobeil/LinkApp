using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using Firebase;
using Firebase.Auth;
using Android.Gms.Common.Apis;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Content;
using LinkMobile.Views;

namespace LinkMobile.Droid
{
    [Activity(Label = "LinkMobile", Icon = "@drawable/start_icon", Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public int GOOGLE_SIGN_ID = 9001;
        public static FirebaseApp app;
        public FirebaseAuth auth;
        public GoogleApiClient apiClient;
        public GoogleSignInClient googleSignInClient;

        private ConnectionResult connectionResult;
        private bool intentinProgress;
        private bool infoPopulated;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App());

        }

    }
 
}