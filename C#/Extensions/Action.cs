using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace .Extensions
{
    public static class ActionExtensions
    {
        public static void DispatchSafely(this Action action, Dispatcher dispatcher)
            => DispatchSafely(action, dispatcher, DispatcherPriority.Normal);

        public static void DispatchSafely(this Action action, Dispatcher dispatcher, DispatcherPriority priority)
        {
            if (dispatcher == null) { throw new ArgumentNullException(nameof(dispatcher)); }
            if (action == null) { throw new ArgumentNullException(nameof(action)); }

            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.Invoke(action, priority);
            }
        }
    }
}