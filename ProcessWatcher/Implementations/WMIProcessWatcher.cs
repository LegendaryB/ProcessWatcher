using System.Threading.Tasks;
using ProcessWatcher.Implementations.Abstract;
using ProcessWatcher.Infrastructure.Interfaces;

namespace ProcessWatcher.Implementations
{
    internal class WMIProcessWatcher : ProcessWatcherBase,
        IProcessWatcher
    {
        public async Task WatchAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
