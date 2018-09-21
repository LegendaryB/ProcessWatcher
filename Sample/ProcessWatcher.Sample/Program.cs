using ProcessWatcher.Factories;
using System;

namespace ProcessWatcher.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var watcher = ProcessWatcherFactory
                .GetImplementation();

            watcher.RegisterCallbackDelegate((wrapper) =>
            {
                Console.WriteLine();
            });

            watcher.WatchAsync().Wait();
        }
    }
}
