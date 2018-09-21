using ProcessWatcher.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
