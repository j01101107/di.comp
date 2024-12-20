﻿using System;
using System.Collections.Generic;

using Castle.Core;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;

namespace Castle.Common.Facilities.EventWiring
{
    public class EventWiringInspector : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            var subscribersConfiguration = GetSubscribersConfiguration(model);
            if (subscribersConfiguration == null)
            {
                return;
            }

            if (subscribersConfiguration.Children.Count < 1)
            {
                var message = string.Format("The subscribers node must have at least one subsciber child. Check node subscribers of the '{0}' component",
                                            model.Name);
                throw new EventWiringException(message);
            }

            var subscribers2Evts = new Dictionary<string, List<WireInfo>>();
            foreach (var subscriber in subscribersConfiguration.Children)
            {
                var subscriberKey = GetSubscriberKey(subscriber);
                ExtractAndAddEventInfo(subscribers2Evts, subscriberKey, subscriber, model);
            }

            AddSubscriberDependecyToModel(subscribers2Evts.Keys, model);
            model.ExtendedProperties[EventWiringFacility.SubscriberList] = subscribers2Evts;
        }

        private void AddSubscriberDependecyToModel(IEnumerable<string> subscribers, ComponentModel model)
        {
            foreach (var subscriber in subscribers)
            {
                model.Dependencies.Add(new ComponentDependencyModel(subscriber));
            }
        }

        private void ExtractAndAddEventInfo(IDictionary<string, List<WireInfo>> subscribers2Evts, string subscriberKey, IConfiguration subscriber,
                                            ComponentModel model)
        {
            List<WireInfo> wireInfoList;
            if (subscribers2Evts.TryGetValue(subscriberKey, out wireInfoList) == false)
            {
                wireInfoList = new List<WireInfo>();
                subscribers2Evts[subscriberKey] = wireInfoList;
            }

            var eventName = subscriber.Attributes["event"];
            if (string.IsNullOrEmpty(eventName))
            {
                throw new EventWiringException("You must supply an 'event' " +
                                               "attribute which is the event name on the publisher you want to subscribe." +
                                               " Check node 'subscriber' for component " + model.Name + "and id = " + subscriberKey);
            }

            var handlerMethodName = subscriber.Attributes["handler"];
            if (string.IsNullOrEmpty(handlerMethodName))
            {
                throw new EventWiringException("You must supply an 'handler' attribute " +
                                               "which is the method on the subscriber that will handle the event." +
                                               " Check node 'subscriber' for component " + model.Name + "and id = " + subscriberKey);
            }

            wireInfoList.Add(new WireInfo(eventName, handlerMethodName));
        }

        private static string GetSubscriberKey(IConfiguration subscriber)
        {
            var subscriberKey = subscriber.Attributes["id"];

            if (string.IsNullOrEmpty(subscriberKey))
            {
                throw new EventWiringException("The subscriber node must have a valid Id assigned");
            }

            return subscriberKey;
        }

        private static IConfiguration GetSubscribersConfiguration(ComponentModel model)
        {
            if (model.Configuration == null)
            {
                return null;
            }

            return model.Configuration.Children["subscribers"];
        }
    }
}