using ProcessWatcher.Factories;

namespace ProcessWatcher.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var watcher = ProcessWatcherFactory
                .GetImplementation();

            watcher.WatchAsync();
        }
    }
}
