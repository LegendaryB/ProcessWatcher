using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessWatcher
{
    public class ProcessEqualityComparer : 
        IEqualityComparer<Process>
    {
        public bool Equals(Process x, Process y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Id == y.Id && 
                x.ProcessName == y.ProcessName && 
                x.SessionId == y.SessionId;
        }

        public int GetHashCode(Process obj)
        {
            if (obj == null)
                return 0;

            return obj.Id;
        }
    }
}
