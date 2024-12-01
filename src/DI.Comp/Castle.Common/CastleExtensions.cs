using System;
using System.Collections;
using System.ComponentModel;

using Castle.MicroKernel;

namespace Castle.Common
{
    public static class CastleExtensions
    {
        public static Arguments ToArguments(this IDictionary parameters)
        {
            var result = new Arguments();
            foreach (DictionaryEntry dictionaryEntry in parameters)
            {
                result.Add(dictionaryEntry.Key, dictionaryEntry.Value);
            }

            return result;
        }

        public static Arguments ToArguments(this object argumentsAsAnonymousType)
        {
            var result = new Arguments();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(argumentsAsAnonymousType))
            {
                var value = descriptor.GetValue(argumentsAsAnonymousType);
                result.Add(descriptor.Name, value);
            }

            return result;
        }
    }
}