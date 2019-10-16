using LinkMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Services.Interfaces
{
    public interface IGoogleUsersData
    {
        User GetGoogleUsersData();

        void ClearGoogleUserData();
    }
}
