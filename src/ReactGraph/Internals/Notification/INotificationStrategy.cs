using System;

namespace ReactGraph.Internals.Notification
{
    internal interface INotificationStrategy
    {
        void Track(object instance);
        void Untrack(object instance);
        bool AppliesTo(Type type);
    }
}