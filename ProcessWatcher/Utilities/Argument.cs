using System;

namespace ProcessWatcher.Utilities
{
    internal static class Argument
    {
        internal static void IsNotNull(
            object param,
            string paramName,
            string message = "")
        {
            if (param == null)
                throw new ArgumentNullException(
                    paramName,
                    message);
        }

        internal static void IsNotNullOrWhitespace(
            string param,
            string paramName,
            string message = "")
        {
            IsNotNull(param, paramName, message);

            if (string.IsNullOrWhiteSpace(param))
                throw new ArgumentException(
                    paramName,
                    message);
        }
    }
}
