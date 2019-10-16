using LinkMobile.Services.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace LinkMobile.Services.Persistence
{
    public class PersistenceService : IPersistenceService
    {
        public object GetPersistenceValueWithKey(string key)
        {
            object value;
            value = Preferences.Get(key, "error");
            return value;
        }

        public void SetPersistenceValueAndKey(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public void RemovePersistentKey(string key)
        {
            Preferences.Remove(key);
        }

        public void ClearPreferences()
        {
            Preferences.Clear();
        }
    }
}
