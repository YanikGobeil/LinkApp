using LinkMobile.Network;
using LinkMobile.Network.Response;
using LinkMobile.Services.Interfaces;
using LinkMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LinkMobile.Services
{
    public class NetworkService : INetworkService
    {
        private const string RootUrl = "https://link-udes-api.herokuapp.com/api/";
        private const string ContentType = "application/json";
        private const string TokenKey = "jwt";
        private const string Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE1NTEzMzAwMDAsInVzZXIiOiJ7XCJjaXBcIjpcInRlc3QxMjM0XCIsXCJub21cIjpcIkJlYXVsaWV1XCIsXCJwcmVub21cIjpcIlBhdHJpY2tcIixcImVtYWlsXCI6XCJQYXRyaWNrLkJlYXVsaWV1M0BVU2hlcmJyb29rZS5jYVwifSJ9.8_Dc3XnA6j5ill93GI4HGSPChPwcBv6sbXdJcAU54FI";

        private readonly Dictionary<string, string> _header = new Dictionary<string, string>();
        private readonly RestClient _restClient = new RestClient(ContentType);

        private IMasterNavigationService _masterNavigationService;

        public NetworkService(IMasterNavigationService masterNavigationService)
        {
            _masterNavigationService = masterNavigationService;

            if (Application.Current.Properties.TryGetValue(TokenKey, out var token))
            {
                _header[TokenKey] = token.ToString();
            }

            _header[TokenKey] = Token; // Comment this to use real CIP
        }

        public async Task SetToken(string token)
        {
            _header[TokenKey] = token;
            Application.Current.Properties[TokenKey] = token;
            await Application.Current.SavePropertiesAsync();
        }

        public async Task<TResp> GetAsync<TResp>(string path, CancellationToken cancellationToken) where TResp : BaseResponse
        {
            return await Execute(async () => await _restClient.GetAsync<TResp>(new Uri($"{RootUrl}{path}"), _header, cancellationToken));
        }

        public async Task<List<TResp>> GetListAsync<TResp>(string path, CancellationToken cancellationToken)
        {
            return await Execute(async () => await _restClient.GetAsync<List<TResp>>(new Uri($"{RootUrl}{path}"), _header, cancellationToken));
        }

        public async Task<TResp> PostAsync<TReq, TResp>(string path, TReq requestObject, CancellationToken cancellationToken) where TResp : BaseResponse
        {
            return await Execute(async () => await _restClient.PostAsync<TReq, TResp>(new Uri($"{RootUrl}{path}"), requestObject, _header, cancellationToken));
        }

        public async Task<TResp> PutAsync<TReq, TResp>(string path, TReq requestObject, CancellationToken cancellationToken) where TResp : BaseResponse
        {
            return await Execute(async () => await _restClient.PutAsync<TReq, TResp>(new Uri($"{RootUrl}{path}"), requestObject, _header, cancellationToken));
        }

        private async Task<TResp> Execute<TResp>(Func<Task<TResp>> request)
        {
            try
            {
                return await request?.Invoke();
            }
            catch (Exception e) when (e is ApiException ae && (ae.StatusCode == ApiStatus.Unauthorized || ae.StatusCode == ApiStatus.Forbidden))
            {
                _masterNavigationService.SetMainPage(new HomePage());
                throw;
            }
        }
    }
}
