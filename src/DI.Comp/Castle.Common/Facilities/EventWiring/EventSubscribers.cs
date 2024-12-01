using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Castle.Core;

namespace Castle.Common.Facilities.EventWiring
{
    public class EventSubscribers
    {
        private readonly List<EventSubscriber> subscribers = new List<EventSubscriber>(3);

        internal EventSubscriber[] Subscribers => subscribers.ToArray();

        public EventSubscribers To<TSubscriber>(string subscriberComponentName, Expression<Action<TSubscriber>> methodHandler)
        {
            subscribers.Add(EventSubscriber.Named(subscriberComponentName).HandledBy(methodHandler));
            return this;
        }

        public EventSubscribers To<TSubscriber>(Expression<Action<TSubscriber>> methodHandler)
        {
            return To(ComponentName.DefaultNameFor(typeof(TSubscriber)), methodHandler);
        }

        public EventSubscribers To<TSubscriber>(string methodHandler)
        {
            return To(ComponentName.DefaultNameFor(typeof(TSubscriber)), methodHandler);
        }

        public EventSubscribers To<TSubscriber>()
        {
            return To(ComponentName.DefaultNameFor(typeof(TSubscriber)));
        }

        public EventSubscribers To(string subscriberComponentName, string methodHandler)
        {
            subscribers.Add(EventSubscriber.Named(subscriberComponentName).HandledBy(methodHandler));
            return this;
        }

        public EventSubscribers To(string subscriberComponentName)
        {
            subscribers.Add(EventSubscriber.Named(subscriberComponentName));
            return this;
        }
    }
}