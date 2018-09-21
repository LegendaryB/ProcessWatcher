using ProcessWatcher.Implementations.Abstract;
using ProcessWatcher.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProcessWatcher.Implementations
{
    internal class PoorManProcessWatcher : ProcessWatcherBase,
        IProcessWatcher
    {
        public async Task WatchAsync()
        {
            throw new NotImplementedException();
        }
    }
}
