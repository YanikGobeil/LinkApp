using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinkMobile.Services.Interfaces
{
    public interface IAuthentificationService
    {
        Task<string> LoginWithEmailPassword(string email, string password);

        Task GoogleLoginWithCache();

    }
}
