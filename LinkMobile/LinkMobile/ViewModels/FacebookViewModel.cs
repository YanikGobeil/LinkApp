using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LinkMobile.Models;
using LinkMobile.Services;
using LinkMobile.Services.Interfaces;
using LinkMobile.Static;
using LinkMobile.Views;

namespace LinkMobile.ViewModels
{
    public class FacebookViewModel : INotifyPropertyChanged
    {
        private FacebookProfile _facebookProfile;

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
            var facebookServices = new FacebookService();

            try
            {
                FacebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
                StaticValues.staticFacebookProfile = FacebookProfile;
                StaticValues.currentUser = new User()
                {
                    email = FacebookProfile.Email,
                    firstName = FacebookProfile.FirstName,
                    lastName = FacebookProfile.LastName
                };
            }
            catch(Exception e)
            {
                
            }
           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
