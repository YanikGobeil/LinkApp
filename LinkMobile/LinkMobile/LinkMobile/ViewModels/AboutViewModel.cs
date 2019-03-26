using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace LinkMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
         
        }

        public ICommand OpenWebCommand { get; }
    }
}