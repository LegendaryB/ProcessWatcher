using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessWatcher
{
    public class Watchdog
    {
        private IEnumerable<Process> _processListSnapshot;

        private IEqualityComparer<Process> _processEqualityComparer =
            new ProcessEqualityComparer();

        private ConcurrentDictionary<Guid, Action<Process>> _callbacks =
            new ConcurrentDictionary<Guid, Action<Process>>();

        public void Attach(
            Action<Process> callback,
            out Guid callbackId)
        {
            callbackId = Guid.Empty;

            if (callback == null)
                return;

            callbackId = Guid.NewGuid();

            if (!_callbacks.TryAdd(
                callbackId,
                callback))
            {
                callbackId = Guid.Empty;
                return;
            }
        }

        public bool Detach(
            Guid callbackId)
        {
            if (callbackId == Guid.Empty)
                return true;

            return _callbacks.TryRemove(
                callbackId,
                out Action<Process> callback);
        }

        public async Task Run()
        {
            _processListSnapshot = Process
                .GetProcesses();

            while (true)
            {
                await UpdateProcessListSnapshot();
                await Task.Delay(5000);
            }
        }

        private async Task UpdateProcessListSnapshot()
        {
            await Task.Run(() =>
            {
                var processes = Process
                    .GetProcesses();

                var addedProcesses = processes
                    .Except(
                        _processListSnapshot,
                        _processEqualityComparer);

                if (!addedProcesses.Any())
                    return;

                NotifySubscribers(addedProcesses);

                _processListSnapshot = processes;
            })
            .ConfigureAwait(false);
        }

        private void NotifySubscribers(
            IEnumerable<Process> addedProcesses)
        {
            foreach (var process in addedProcesses)
            {
                foreach (var callback in _callbacks)
                    callback.Value?.Invoke(process);
            }
        }
    }
}
