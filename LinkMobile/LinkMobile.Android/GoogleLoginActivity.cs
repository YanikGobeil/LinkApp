using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using LinkMobile.Droid.Models;
using LinkMobile.Views;
using Xamarin.Forms;

namespace LinkMobile.Droid
{
    [Activity(Label = "Redirection Google", NoHistory = true)]
    public class GoogleLoginActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public int GOOGLE_SIGN_ID = 7;
        public static FirebaseApp app;
        public FirebaseAuth auth;
        public GoogleApiClient apiClient;
        public GoogleSignInClient googleSignInClient;

        private User currentUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ConfigureGoogleSignInWithCache();
            StaticAuth.api = apiClient;
        }

        public void ConfigureGoogleSignInWithCache()
        {
            GoogleSignInOptions options = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestProfile().RequestEmail().Build();

            apiClient = new GoogleApiClient.Builder(this)
                               .AddConnectionCallbacks(this)
                               .AddOnConnectionFailedListener(this)
                               .AddApi(Auth.GOOGLE_SIGN_IN_API, options)
                               .Build();

            getConnectionResult();
        }

        public void getConnectionResult()
        {
            var signInIntent = Auth.GoogleSignInApi.GetSignInIntent(apiClient);
            StartActivityForResult(signInIntent, GOOGLE_SIGN_ID);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == GOOGLE_SIGN_ID)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                HandleSignInResult(result);
            }
        }

        private void HandleSignInResult(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {
                var accountDetails = result.SignInAccount;
                setAccountDetailsAsUser(accountDetails);

                //close activity
                Finish();              
            }
        }

        private void setAccountDetailsAsUser(GoogleSignInAccount details)
        {
            currentUser = new User();
            currentUser.email = details.Email;
            currentUser.firstName = details.DisplayName;
            currentUser.lastName = details.FamilyName;
            StaticAuth.currentUser = currentUser;
        }
        public void OnConnected(Bundle connectionHint)
        {
            //called when connected to google api client
        }

        public void OnConnectionSuspended(int cause)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            //called when the connection to google api client fails
        }

    }
}