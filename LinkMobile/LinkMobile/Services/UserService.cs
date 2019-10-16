using LinkMobile.Network.Request;
using LinkMobile.Network.Response;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkMobile.Services
{
    public class UserService : IUserService
    {
        private INetworkService _networkService;

        public UserService(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public async Task<BaseResponse> CreateUser(string path, PostUserRequest Treq, CancellationToken token)
        {
            var response = await _networkService.PostAsync<PostUserRequest, BaseResponse>(path, Treq, token);
            return response;
        }

        public async Task<List<UserResponse>> GetAllUsers(string path, CancellationToken token)
        {
            var response = await _networkService.GetListAsync<UserResponse>(path, token);
            return response; ;
        }

        public async Task<UserResponse> GetUserByEmail(string path, string userEmail, CancellationToken token)
        {
            var response = await _networkService.GetAsync<UserResponse>($"usersByEmail/" + userEmail, token);
            return response;
        }
    }
}
