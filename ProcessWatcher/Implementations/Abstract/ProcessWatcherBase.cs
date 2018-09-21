using ProcessWatcher.Helper;
using ProcessWatcher.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace ProcessWatcher.Implementations.Abstract
{
    internal abstract class ProcessWatcherBase        
    {
        protected IList<Action<ProcessWrapper>> ProcessStartedCallbackList { get; } =
            new List<Action<ProcessWrapper>>();

        public virtual void RegisterCallbackDelegate(
            Action<ProcessWrapper> onProcessStartedCallbackDelegate)
        {
            Argument.IsNotNull(
                onProcessStartedCallbackDelegate,
                nameof(onProcessStartedCallbackDelegate));

            ProcessStartedCallbackList.Add(
                onProcessStartedCallbackDelegate);
        }
    }
}
