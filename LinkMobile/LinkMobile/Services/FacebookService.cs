using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinkMobile.Models;
using LinkMobile.Services.Interfaces.Persistence;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace LinkMobile.Services
{
    class FacebookService
    {
        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            //clear webviews cache
            DependencyService.Get<IWebCookiesPersistenceService>().RemoveCookies();

            var requestUrl =
                "https://graph.facebook.com/v4.0/me?fields=email,first_name,last_name&access_token="
                + accessToken;


            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            return facebookProfile;
        }

        public async Task Logout(string accessToken, string userId)
        {
            var requestUrl =
                "https://graph.facebook.com/v4.0/" + userId + "/permissions?method=delete&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync(requestUrl);

        }
    }
}
