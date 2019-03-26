using LinkMobile.Network.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkMobile.Services.Interfaces
{
    public interface INetworkService
    {
        Task SetToken(string token);

        Task<TResp> GetAsync<TResp>(string path, CancellationToken cancellationToken) where TResp : BaseResponse;

        Task<List<TResp>> GetListAsync<TResp>(string path, CancellationToken cancellationToken);

        Task<TResp> PostAsync<TReq, TResp>(string path, TReq requestObject, CancellationToken cancellationToken) where TResp : BaseResponse;

        Task<TResp> PutAsync<TReq, TResp>(string path, TReq requestObject, CancellationToken cancellationToken) where TResp : BaseResponse;
    }
}
