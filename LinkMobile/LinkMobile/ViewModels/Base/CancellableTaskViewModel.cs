using LinkMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LinkMobile.ViewModels.Base
{
    public class CancellableTaskViewModel : BaseViewModel
    {
        protected UniqueExecutionTaskQueue _uniqueExecutionTaskQueue;

        public ICommand CancelRunningTaskCommand => new Command(async () => await _uniqueExecutionTaskQueue.Cancel());

        public CancellableTaskViewModel()
        {
            _uniqueExecutionTaskQueue = new UniqueExecutionTaskQueue();
        }
    }
}
