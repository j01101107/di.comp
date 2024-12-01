using System;
using System.Linq.Expressions;

namespace Castle.Common.Facilities.EventWiring
{
    public class EventSubscriber
    {
        private EventSubscriber(string subscriberComponentName)
        {
            SubscriberComponentName = subscriberComponentName;
        }

        public string EventHandler { get; private set; }

        public string SubscriberComponentName { get; }

        public EventSubscriber HandledBy(string eventHandlerMethodName)
        {
            EventHandler = eventHandlerMethodName;
            return this;
        }

        public EventSubscriber HandledBy<THandler>(Expression<Action<THandler>> methodHandler)
        {
            EventHandler = ExtractMethodName(methodHandler);
            return this;
        }

        private string ExtractMethodName<THandler>(Expression<Action<THandler>> methodHandler)
        {
            var expression = methodHandler.Body as MethodCallExpression;
            if (expression != null)
            {
                return expression.Method.Name;
            }

            throw new ArgumentException(
                                        "Couldn't extract method to handle the event from given expression. Expression should point to method that ought to handle subscribed event, something like: 's => s.HandleClick(null, null)'.");
        }

        public static EventSubscriber Named(string subscriberComponentName)
        {
            return new EventSubscriber(subscriberComponentName);
        }
    }
}