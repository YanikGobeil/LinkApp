using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Firebase.Auth;
using LinkMobile.Droid;
using LinkMobile.Services;
using LinkMobile.Services.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(GoogleLoginService))]
namespace LinkMobile.Droid
{
    public class GoogleLoginService : IAuthentificationService 
    {
        public IntPtr Handle => throw new NotImplementedException();

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var currentApp = StaticAuth.firebaseAppList[0];
                FirebaseAuth auth = FirebaseAuth.GetInstance(currentApp);
                if (auth == null)
                {
                    auth = new FirebaseAuth(currentApp);
                }
                var user = await auth.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }

        
        public async Task GoogleLoginWithCache()
        {
            try
            {
                
                
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                
            }

            
        }   
    }
}