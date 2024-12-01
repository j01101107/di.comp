using System;
using System.Reflection;

using Castle.MicroKernel.Registration;

namespace Castle.Common.Facilities.EventWiring
{
    public static class EventWiringRegistrationExtensions
    {
        public static ComponentRegistration<TPublisher> PublishEvent<TPublisher>(this ComponentRegistration<TPublisher> registration, string eventName,
                                                                                 Action<EventSubscribers> toSubscribers)
            where TPublisher : class
        {
            var subscribers = new EventSubscribers();
            toSubscribers(subscribers);

            return registration.AddDescriptor(new EventWiringDescriptor(eventName, subscribers.Subscribers));
        }

        public static ComponentRegistration PublishEvent(this ComponentRegistration registration, string eventName, Action<EventSubscribers> toSubscribers)
        {
            var subscribers = new EventSubscribers();
            toSubscribers(subscribers);

            registration.AddDescriptor(new EventWiringDescriptor(eventName, subscribers.Subscribers));
            return registration;
        }

        public static ComponentRegistration<TPublisher> PublishEvent<TPublisher>(this ComponentRegistration<TPublisher> registration,
                                                                                 Action<TPublisher> eventSubscribtion,
                                                                                 Action<EventSubscribers> toSubscribers)
            where TPublisher : class
        {
            var eventName = GetEventName(eventSubscribtion);

            var subscribers = new EventSubscribers();
            toSubscribers(subscribers);

            return registration.AddDescriptor(new EventWiringDescriptor(eventName, subscribers.Subscribers));
        }

        public static ComponentRegistration PublishEvent<TPublisher>(this ComponentRegistration registration, Action<TPublisher> eventSubscribtion,
                                                                     Action<EventSubscribers> toSubscribers)
        {
            var eventName = GetEventName(eventSubscribtion);

            var subscribers = new EventSubscribers();
            toSubscribers(subscribers);

            registration.AddDescriptor(new EventWiringDescriptor(eventName, subscribers.Subscribers));
            return registration;
        }

        private static string GetEventName<TPublisher>(Action<TPublisher> eventSubscribtion)
        {
            string eventName;
            try
            {
                var calledMethod = new NaiveMethodNameExtractor(eventSubscribtion).CalledMethod;
                if (calledMethod == null || (eventName = ExtractEventName(calledMethod)) == null)
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException(
                                            "Delegate given was not a method subscription delegate. Please use something similar to: 'publisher => publisher += null'. " +
                                            "If you did, than it's probably a bug. Please use the other overload and specify name of the event as string.");
            }

            return eventName;
        }

        private static string ExtractEventName(MethodBase calledMethod)
        {
            var methodName = calledMethod.Name;
            if (methodName.StartsWith("add_"))
            {
                return methodName.Substring("add_".Length);
            }

            if (methodName.StartsWith("remove_"))
            {
                return methodName.Substring("remove_".Length);
            }

            return null;
        }
    }
}