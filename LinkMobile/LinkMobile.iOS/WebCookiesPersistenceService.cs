using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using LinkMobile.iOS;
using LinkMobile.Services.Interfaces.Persistence;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebCookiesPersistenceService))]
namespace LinkMobile.iOS
{
    public class WebCookiesPersistenceService : IWebCookiesPersistenceService
    {
        public void RemoveCookies()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
                CookieStorage.DeleteCookie(cookie);
        }
    }
    
}