using ProcessWatcher.Utilities;
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

        protected virtual void InvokeCallbackDelegates(
            ProcessWrapper processWrapper)
        {
            foreach (var callback in ProcessStartedCallbackList)
                callback?.Invoke(processWrapper);
        }
    }
}
