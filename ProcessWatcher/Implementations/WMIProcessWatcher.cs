using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using ProcessWatcher.Implementations.Abstract;
using ProcessWatcher.Infrastructure.Interfaces;
using ProcessWatcher.Infrastructure.Models;

namespace ProcessWatcher.Implementations
{
    internal class WMIProcessWatcher : ProcessWatcherBase,
        IProcessWatcher
    {
        private const string WMI_SCOPE = @"\\.\root\CIMV2";
        private const string WMI_BASE_QUERY = 
            "SELECT * " +
            "FROM __InstanceCreationEvent " +
            "WITHIN 10 " +
            "WHERE TargetInstance ISA 'Win32_Process'";

        public async Task WatchAsync()
        {
            await Task.Run(() =>
            {
                var eventWatcher = GetWmiEventWatcher();

                while (true)
                    eventWatcher.WaitForNextEvent();
            }).ConfigureAwait(false);
        }

        private ManagementEventWatcher GetWmiEventWatcher()
        {
            var eventWatcher = new ManagementEventWatcher(
                WMI_SCOPE,
                WMI_BASE_QUERY);

            eventWatcher.EventArrived += OnEventArrived;
            eventWatcher.Start();

            return eventWatcher;
        }

        private void OnEventArrived(
            object sender, 
            EventArrivedEventArgs e)
        {
            if (!(e.NewEvent.Properties["TargetInstance"].Value is ManagementBaseObject instance))
                return;

            var properties = instance
                .Properties
                .OfType<PropertyData>()
                .ToDictionary(prop => prop.Name, prop => prop.Value);

            var processWrapper = CreateProcessWrapper(
                properties);

            if (processWrapper == null)
                return;

            InvokeCallbackDelegates(processWrapper);
        }

        private ProcessWrapper CreateProcessWrapper(
            IDictionary<string, object> extendedProperties)
        {
            var processIdProperty = extendedProperties
                .FirstOrDefault(ep => ep.Key == "ProcessId");

            if (processIdProperty.Equals(default(KeyValuePair<string, object>)))
                return null;

            string processIdString = processIdProperty
                .Value?
                .ToString();

            if (!int.TryParse(processIdString, out int processId))
                return null;

            var process = Process.GetProcessById(processId);

            return new ProcessWrapper
            {
                Process = process,
                ExtendedProperties = extendedProperties
            };                        
        }
    }
}
