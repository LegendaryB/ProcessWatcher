using ProcessWatcher.Infrastructure.Interfaces;
using System.Security.Principal;

namespace ProcessWatcher
{
    public static class ProcessWatcherFactory
    {
        public static IProcessWatcher GetImplementation()
        {
            IProcessWatcher implementation;

            if (IsRunningAsAdmin())
                implementation = new WMIProcessWatcher();
            else
                implementation = new PoorManProcessWatcher();

            return implementation;
        }

        private static bool IsRunningAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
