using ProcessWatcher.Infrastructure.Models;
using System;

namespace ProcessWatcher.Infrastructure.Interfaces
{
    public interface IProcessWatcher
    {
        // todo: use extended class
        void RegisterCallbackDelegate(
            Action<ProcessWrapper> onProcessStartedCallbackDelegate);
    }
}
