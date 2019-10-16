using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using LinkMobile.iOS;
using LinkMobile.Models;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(GoogleUsersDataHandler))]
namespace LinkMobile.iOS
{
    public class GoogleUsersDataHandler
    {
        public User GetGoogleUsersData()
        {
            try
            {
                User user = new User();

                return user;
            }
            catch
            {
                throw new NullReferenceException();
            }
        }

        public void ClearGoogleUserData()
        {
            //
        }
    }

}