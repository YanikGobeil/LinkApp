using LinkMobile.Network.Request;
using LinkMobile.Network.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkMobile.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse> CreateUser(string path, PostUserRequest Treq, CancellationToken token);

        Task<UserResponse> GetUserByEmail(string path, string userEmail, CancellationToken token);

        Task<List<UserResponse>> GetAllUsers(string path, CancellationToken token);
    }
}
