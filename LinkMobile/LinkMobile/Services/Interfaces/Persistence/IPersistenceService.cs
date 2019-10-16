using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Services.Interfaces.Persistence
{
    public interface IPersistenceService
    {
        object GetPersistenceValueWithKey(string key);


        void SetPersistenceValueAndKey(string key, string value);


        void RemovePersistentKey(string key);

        void ClearPreferences();
    }
}
