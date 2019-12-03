using LinkMobile.Models;
using LinkMobile.Network.Request;
using LinkMobile.Services.Interfaces;
using LinkMobile.Services.Interfaces.Persistence;
using LinkMobile.Static;
using LinkMobile.ViewModels.Base;
using LinkMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LinkMobile.ViewModels
{
    public class HomePageViewModel : CancellableTaskViewModel
    {
        public const string PATH_USERS = "/users";

        public bool isInitialized = false;
        private IMasterNavigationService _masterNavigationService;
        private IReservationService _reservationService;
        private IPersistenceService _persistenceService;
        private IPageService _pageService;
        private IUserService _userService;

        public ICommand OnBackCommand { get; private set; }

        public ICommand UpdateCommand => new Command(async () => await UpdateOnAppearing());

        public MyReservationsViewModel MyReservationsViewModel { get; private set; }

        public HomePageViewModel(IMasterNavigationService masterNavigationService, IReservationService reservationService, IPageService pageService, IUserService userService, IPersistenceService persistenceService)
        {
            MyReservationsViewModel = new MyReservationsViewModel(pageService, _uniqueExecutionTaskQueue);
            _masterNavigationService = masterNavigationService;
            _reservationService = reservationService;
            _persistenceService = persistenceService;
            _pageService = pageService;
            _userService = userService;
            
            OnBackCommand = new Command(async () => await StayOnPage());

        }

        private async Task StayOnPage()
        {
            await _masterNavigationService.NavigateToPage(new HomePage(isInitialized));
        }

        private async Task UpdateOnAppearing()
        {
            if (!isInitialized)
            {
                PostUserRequest request = await RegisterUser();
                if (request.email != null)
                {
                    PersistUser(request);
                }
                isInitialized = true;
            }
        
            //GetUserActiveRegistrations();
        }

        private async Task<PostUserRequest> RegisterUser()
        {
            PostUserRequest request = new PostUserRequest();
            request = await Task.Run(async () =>
            {
                


                if (StaticValues.currentUser != null && StaticValues.staticFacebookProfile == null)
                {
                    try
                    {
                        request.email = StaticValues.currentUser.email;
                        request.firstName = StaticValues.currentUser.firstName;
                        request.lastName = StaticValues.currentUser.lastName;
                        CancellationToken token = new CancellationToken();

                        var condition = await _userService.GetUserByEmail(PATH_USERS, request.email, token);
                        if (condition == null)
                        {
                            var response = await _userService.CreateUser(PATH_USERS, request, token);
                        }

                    }
                    catch (Exception e)
                    {
                        //await _pageService.DisplayAlert("Erreur de requête", e.Message, "OK");
                    }
                }
                else if (StaticValues.currentUser == null && StaticValues.staticFacebookProfile != null)
                {
                    try
                    {
                        if (StaticValues.staticFacebookProfile.Email == null && StaticValues.currentUser == null)
                        {
                            _persistenceService.SetPersistenceValueAndKey("email", "yanik.gobeil@hotmail.com");
                        }

                        request.email = StaticValues.staticFacebookProfile.Email;
                        request.firstName = StaticValues.staticFacebookProfile.FirstName;
                        request.lastName = StaticValues.staticFacebookProfile.LastName;
                        CancellationToken token = new CancellationToken();
                        var condition = await _userService.GetUserByEmail(PATH_USERS, request.email, token);
                        if (condition == null)
                        {
                            var response = await _userService.CreateUser(PATH_USERS, request, token);
                        }

                    }
                    catch (Exception e)
                    {
                       // await _pageService.DisplayAlert("Erreur de requête", e.Message, "OK");
                    }
                }
                else
                {
                    if (_persistenceService.GetPersistenceValueWithKey("email") == null)
                    {
                       // await _pageService.DisplayAlert("Erreur native", "Impossible d'identifier l'utilisateur correctement.", "OK");
                    }
                }

                return request;
            });
            return request;
        }
     
        private void PersistUser(PostUserRequest request)
        {
            try
            {
                object persistenceResponse = _persistenceService.GetPersistenceValueWithKey("email");
                if (persistenceResponse.Equals("error"))
                {
                    _persistenceService.SetPersistenceValueAndKey("email", request.email);
                }

            }
            catch (Exception e)
            {
               // _pageService.DisplayAlert("Erreur de persistence", e.Message ,"OK");
            }
            
        }

        private async Task GetUserActiveRegistrations()
        {
            await _uniqueExecutionTaskQueue.Execute(async (token) =>
            {              
                await MyReservationsViewModel.UpdateMyReservationsList(async () =>
                {
                    return await _reservationService.GetUserActiveReservations("", "goby2801", token);
                }
                , token);
            });
        }
    }
}
