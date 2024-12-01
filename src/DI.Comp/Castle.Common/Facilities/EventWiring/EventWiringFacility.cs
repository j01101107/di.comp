using System;
using System.Collections;
using System.Reflection;

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;

namespace Castle.Common.Facilities.EventWiring
{
    public class EventWiringFacility : AbstractFacility
    {
        internal const string SubscriberList = "evts.subscriber.list";

        /// <summary>
        ///     Overridden. Initializes the facility, subscribing to the <see cref="IKernelEvents.ComponentModelCreated" />,
        ///     <see cref="IKernelEvents.ComponentCreated" />, <see cref="IKernelEvents.ComponentDestroyed" /> Kernel events.
        /// </summary>
        protected override void Init()
        {
            Kernel.ComponentModelBuilder.AddContributor(new EventWiringInspector());
            Kernel.ComponentCreated += OnComponentCreated;
            Kernel.ComponentDestroyed += OnComponentDestroyed;
        }

        private bool IsPublisher(ComponentModel model)
        {
            return model.ExtendedProperties[SubscriberList] != null;
        }

        /// <summary>
        ///     Checks if the component we're dealing is a publisher. If it is,
        ///     iterates the subscribers starting them and wiring the events.
        /// </summary>
        /// <param name="model">The component model.</param>
        /// <param name="instance">The instance representing the component.</param>
        /// <exception cref="EventWiringException">
        ///     When the subscriber is not found
        ///     <br /> or <br />
        ///     The handler method isn't found
        ///     <br /> or <br />
        ///     The event isn't found
        /// </exception>
        private void OnComponentCreated(ComponentModel model, object instance)
        {
            if (IsPublisher(model))
            {
                WirePublisher(model, instance);
            }
        }

        private void OnComponentDestroyed(ComponentModel model, object instance)
        {
            // TODO: Remove Listener
        }

        private void StartAndWirePublisherSubscribers(ComponentModel model, object publisher)
        {
            var subscribers = (IDictionary) model.ExtendedProperties[SubscriberList];

            if (subscribers == null)
            {
                return;
            }

            foreach (DictionaryEntry subscriberInfo in subscribers)
            {
                var subscriberKey = (string) subscriberInfo.Key;

                var wireInfoList = (IList) subscriberInfo.Value;

                var handler = Kernel.GetHandler(subscriberKey);

                AssertValidHandler(handler, subscriberKey);

                object subscriberInstance;

                try
                {
                    subscriberInstance = Kernel.Resolve<object>(subscriberKey);
                }
                catch (Exception ex)
                {
                    throw new EventWiringException("Failed to start subscriber " + subscriberKey, ex);
                }

                var publisherType = publisher.GetType();

                foreach (WireInfo wireInfo in wireInfoList)
                {
                    var eventName = wireInfo.EventName;

                    //TODO: Caching of EventInfos.
                    var eventInfo = publisherType.GetEvent(eventName,
                                                           BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (eventInfo == null)
                    {
                        throw new EventWiringException(
                                                       string.Format("Could not find event '{0}' on component '{1}'. Make sure you didn't misspell the name.", eventName, model.Name));
                    }

                    var handlerMethod = subscriberInstance.GetType().GetMethod(wireInfo.Handler,
                                                                               BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (handlerMethod == null)
                    {
                        throw new EventWiringException(
                                                       string.Format(
                                                                     "Could not find method '{0}' on component '{1}' to handle event '{2}' published by component '{3}'. Make sure you didn't misspell the name.",
                                                                     wireInfo.Handler,
                                                                     subscriberKey,
                                                                     eventName,
                                                                     model.Name));
                    }

                    var delegateHandler = subscriberInstance.GetType().GetMethod(wireInfo.Handler).CreateDelegate(eventInfo.EventHandlerType, subscriberInstance);

                    eventInfo.AddEventHandler(publisher, delegateHandler);
                }
            }
        }

        private void WirePublisher(ComponentModel model, object publisher)
        {
            StartAndWirePublisherSubscribers(model, publisher);
        }

        private static void AssertValidHandler(IHandler handler, string subscriberKey)
        {
            if (handler == null)
            {
                throw new EventWiringException("Publisher tried to start subscriber " + subscriberKey + " that was not found");
            }

            if (handler.CurrentState == HandlerState.WaitingDependency)
            {
                throw new EventWiringException("Publisher tried to start subscriber " + subscriberKey + " that is waiting for a dependency");
            }
        }
    }

    /// <summary>
    ///     Represents the information about an event.
    /// </summary>
    internal class WireInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WireInfo" /> class.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="handler">The name of the handler method.</param>
        public WireInfo(string eventName, string handler)
        {
            EventName = eventName;
            Handler = handler;
        }

        /// <summary>
        ///     Gets the name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        public string EventName { get; }

        /// <summary>
        ///     Gets the handler method name.
        /// </summary>
        /// <value>The handler.</value>
        public string Handler { get; }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object"></see> is equal to the current
        ///     <see
        ///         cref="T:System.Object">
        ///     </see>
        ///     .
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>
        ///     .
        /// </param>
        /// <returns>
        ///     true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>
        ///     ; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            var wireInfo = obj as WireInfo;
            if (wireInfo == null)
            {
                return false;
            }

            if (!Equals(EventName, wireInfo.EventName))
            {
                return false;
            }

            if (!Equals(Handler, wireInfo.Handler))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override int GetHashCode()
        {
            return EventName.GetHashCode() + 29 * Handler.GetHashCode();
        }
    }
}