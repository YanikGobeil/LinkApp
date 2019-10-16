using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Foundation;
using LinkMobile.iOS;
using LinkMobile.Services;
using LinkMobile.Services.Interfaces;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(GoogleLoginService))]
namespace LinkMobile.iOS
{
    public class GoogleLoginService : IAuthentificationService
    {

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception e)
            {
                return "";
            }

        }

        public Task GoogleLoginWithCache()
        {
            throw new NotImplementedException();
        }
    }
}