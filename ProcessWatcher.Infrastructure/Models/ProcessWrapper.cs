using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessWatcher.Infrastructure.Models
{
    public class ProcessWrapper
    {
        public Process Process { get; set; }
        public IDictionary<string, object> ExtendedProperties { get; set; }
    }
}
