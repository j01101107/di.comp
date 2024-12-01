using System;

using Castle.Core;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Registration;

namespace Castle.Common.Facilities.EventWiring
{
    public class EventWiringDescriptor : IComponentModelDescriptor
    {
        private readonly string eventName;
        private readonly EventSubscriber[] subscribers;

        public EventWiringDescriptor(string eventName, EventSubscriber[] subscribers)
        {
            this.eventName = eventName;
            this.subscribers = subscribers;
        }

        public void BuildComponentModel(IKernel kernel, ComponentModel model)
        {
            var node = GetSubscribersNode(model.Configuration);
            foreach (var eventSubscriber in subscribers)
            {
                var child = Child.ForName("subscriber").Eq(
                                                           Attrib.ForName("id").Eq(eventSubscriber.SubscriberComponentName),
                                                           Attrib.ForName("event").Eq(eventName),
                                                           Attrib.ForName("handler").Eq(EventHandlerMethodName(eventSubscriber)));
                child.ApplyTo(node);
            }
        }

        public void ConfigureComponentModel(IKernel kernel, ComponentModel model)
        {
        }

        private string EventHandlerMethodName(EventSubscriber eventSubscriber)
        {
            return eventSubscriber.EventHandler ?? "On" + eventName;
        }

        private IConfiguration GetSubscribersNode(IConfiguration configuration)
        {
            var node = configuration.Children["subscribers"];
            if (node == null)
            {
                node = new MutableConfiguration("subscribers");
                configuration.Children.Add(node);
            }

            return node;
        }
    }
}