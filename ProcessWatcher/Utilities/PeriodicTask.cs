using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessWatcher.Utilities
{
    internal static class PeriodicTask
    {
        internal static async Task Run(
            Action onTickAction,
            TimeSpan interval,
            CancellationToken token)
        {
            await Task.Delay(interval, token);

            while (!token.IsCancellationRequested)
            {
                onTickAction?.Invoke();

                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, token);
            }
        }
    }
}
