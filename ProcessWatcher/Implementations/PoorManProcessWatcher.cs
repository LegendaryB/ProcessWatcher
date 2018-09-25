using ProcessWatcher.Implementations.Abstract;
using ProcessWatcher.Implementations.EqualityComparer;
using ProcessWatcher.Infrastructure.Interfaces;
using ProcessWatcher.Infrastructure.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessWatcher.Implementations
{
    internal class PoorManProcessWatcher : ProcessWatcherBase,
        IProcessWatcher
    {
        private IEnumerable<Process> _processListSnapshot;
        private IEqualityComparer<Process> _processEqualityComparer =
            new ProcessEqualityComparer();

        public async Task WatchAsync()
        {
            await Task.Run(() =>
            {
                _processListSnapshot = Process
                    .GetProcesses();

                while (true)
                {
                    UpdateProcessListSnapshot();
                    Task.Delay(5000);
                }
            });
        }

        private void UpdateProcessListSnapshot()
        {
            var currentProcessListSnapshot = Process
                .GetProcesses()
                .ToList();

            var addedProcesses = currentProcessListSnapshot
                .Except(
                    _processListSnapshot,
                    _processEqualityComparer)
                .ToList();

            if (!addedProcesses.Any())
                return;

            _processListSnapshot = currentProcessListSnapshot;

            InvokeCallbackDelegates(new ProcessWrapper
            {
                Process = addedProcesses.FirstOrDefault()
            });
        }
    }
}
