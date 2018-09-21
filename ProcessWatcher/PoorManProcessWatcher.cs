using ProcessWatcher.Infrastructure.Interfaces;
using ProcessWatcher.Infrastructure.Models;
using System;

namespace ProcessWatcher
{
    internal class PoorManProcessWatcher :
        IProcessWatcher
    {
        public void RegisterCallbackDelegate(
            Action<ProcessWrapper> onProcessStartedCallbackDelegate)
        {
            throw new NotImplementedException();
        }
    }
}
