using ProcessWatcher.Infrastructure.Models;
using System;
using System.Threading.Tasks;

namespace ProcessWatcher.Infrastructure.Interfaces
{
    public interface IProcessWatcher
    {
        void RegisterCallbackDelegate(
            Action<ProcessWrapper> onProcessStartedCallbackDelegate);

        Task WatchAsync();
    }
}
