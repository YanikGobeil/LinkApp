using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LinkMobile.Droid;
using LinkMobile.Models;
using LinkMobile.Services.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(GoogleUsersDataHandler))]
namespace LinkMobile.Droid
{
    public class GoogleUsersDataHandler : IGoogleUsersData
    {
        public User GetGoogleUsersData()
        {
            if (StaticAuth.currentUser != null)
            {
                try
                {
                    User user = new User();
                    user.firstName = StaticAuth.currentUser.firstName;
                    user.lastName = StaticAuth.currentUser.lastName;
                    user.email = StaticAuth.currentUser.email;
                    user.givenId = StaticAuth.currentUser.givenId;
                    return user;
                }
                catch
                {
                    throw new NullReferenceException();
                }
            }
            else
            {
                return null;
            }
           
        }

        public void ClearGoogleUserData()
        {
            StaticAuth.currentUser = null;
        }
    }
}