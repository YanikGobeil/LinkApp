using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace LinkMobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;

            backingField = value;
            OnPropertyChanged(propertyName);
        }

        protected void SetPropertyValue<T, V>(T target, V value, [CallerMemberName] string propertyName = null)
        {
            var property = typeof(T).GetProperty(propertyName);

            if (EqualityComparer<V>.Default.Equals((V)property.GetValue(target), value))
                return;

            property.SetValue(target, value, null);
            OnPropertyChanged(propertyName);
        }

    }
}
