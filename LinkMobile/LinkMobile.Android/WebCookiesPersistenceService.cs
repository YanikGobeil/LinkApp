using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using LinkMobile.Droid;
using LinkMobile.Services.Interfaces.Persistence;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebCookiesPersistenceService))]
namespace LinkMobile.Droid
{
    public class WebCookiesPersistenceService : IWebCookiesPersistenceService
    {
        public void RemoveCookies()
        {
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
        }
    }
}