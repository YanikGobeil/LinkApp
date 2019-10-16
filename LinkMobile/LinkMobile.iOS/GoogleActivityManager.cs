using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using LinkMobile.iOS;
using LinkMobile.Services.Interfaces;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(GoogleActivityManager))]
namespace LinkMobile.iOS
{
    class GoogleActivityManager : IGoogleSignInPagesManager
    {
        public void DisplayGoogleSignInNativePage()
        {
            //
        }
    }
}