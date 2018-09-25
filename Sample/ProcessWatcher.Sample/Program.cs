using System;

namespace ProcessWatcher.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var watchdog = new Watchdog();

            watchdog.Attach((p) =>
            {
                Console.WriteLine(p.ProcessName);
            },
            out Guid callbackId);

            watchdog.Run();


            Console.ReadLine();
        }
    }
}
