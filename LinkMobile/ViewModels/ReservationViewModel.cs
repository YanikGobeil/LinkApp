using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        private IMasterNavigationService _masterNavigationService;

        public ReservationViewModel(IMasterNavigationService masterNavigationService)
        {

            _masterNavigationService = masterNavigationService;
        }
    }
}
