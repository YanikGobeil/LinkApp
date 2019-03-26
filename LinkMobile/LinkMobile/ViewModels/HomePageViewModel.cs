using LinkMobile.Services.Interfaces;
using LinkMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.ViewModels
{
    public class HomePageViewModel : CancellableTaskViewModel
    {
        private IMasterNavigationService _masterNavigationService;


        public HomePageViewModel(IMasterNavigationService masterNavigationService)
        {
 
            _masterNavigationService = masterNavigationService;
        }

    }
}
