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
using LinkMobile.Services.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(GoogleActivityManager))]
namespace LinkMobile.Droid
{
    public class GoogleActivityManager : IGoogleSignInPagesManager
    {

        public void DisplayGoogleSignInNativePage()
        {

            Intent intent = new Intent(); 
            intent.AddFlags(ActivityFlags.NewTask); 
            intent.AddFlags(ActivityFlags.MultipleTask); 
            intent.SetClass(Android.App.Application.Context, typeof(GoogleLoginActivity));
            Android.App.Application.Context.StartActivity(intent);
        }

    }
}