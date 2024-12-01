using System;
using System.Collections;
using System.Collections.Generic;

using Castle.MicroKernel;
using Castle.Windsor;

namespace Castle.Common
{
    public static class IoC
    {
        private static readonly object _localContainerKey = new object();

        public static IWindsorContainer Container
        {
            get
            {
                var result = LocalContainer ?? GlobalContainer;
                if (result == null)
                {
                    throw new InvalidOperationException("The container has not been initialized! Please call IoC.Initialize(container) before using it.");
                }

                return result;
            }
        }

        private static IWindsorContainer LocalContainer
        {
            get
            {
                if (LocalContainerStack.Count == 0)
                {
                    return null;
                }

                return LocalContainerStack.Peek();
            }
        }

        private static Stack<IWindsorContainer> LocalContainerStack
        {
            get
            {
                var stack = Local.Data[_localContainerKey] as Stack<IWindsorContainer>;
                if (stack == null)
                {
                    Local.Data[_localContainerKey] = stack = new Stack<IWindsorContainer>();
                }

                return stack;
            }
        }

        public static bool IsInitialized => GlobalContainer != null;

        internal static IWindsorContainer GlobalContainer { get; set; }

        public static void Initialize(IWindsorContainer windsorContainer)
        {
            GlobalContainer = windsorContainer;
        }

        public static object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType, new Arguments());
        }

        public static object Resolve(Type serviceType, string serviceName)
        {
            return Container.Resolve(serviceName, serviceType);
        }

        /// <summary>
        ///     Tries to resolve the component, but return null
        ///     instead of throwing if it is not there.
        ///     Useful for optional dependencies.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T TryResolve<T>()
        {
            return TryResolve(default(T));
        }

        /// <summary>
        ///     Tries to resolve the component, but return the default
        ///     value if it could not find it, instead of throwing.
        ///     Useful for optional dependencies.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T TryResolve<T>(T defaultValue)
        {
            if (Container.Kernel.HasComponent(typeof(T)) == false)
            {
                return defaultValue;
            }

            return Container.Resolve<T>();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static T Resolve<T>(string serviceName)
        {
            return Container.Resolve<T>(serviceName);
        }

        public static T Resolve<T>(string serviceName, Arguments arguments)
        {
            return Container.Resolve<T>(serviceName, arguments);
        }

        public static T Resolve<T>(object argumentsAsAnonymousType)
        {
            return Container.Resolve<T>(argumentsAsAnonymousType.ToArguments());
        }

        public static object Resolve<T>(string serviceName, IDictionary parameters)
        {
            return Container.Resolve<T>(serviceName, parameters.ToArguments());
        }

        public static T Resolve<T>(IDictionary parameters)
        {
            return Container.Resolve<T>(parameters.ToArguments());
        }

        /// <summary>
        ///     This allows you to override the global container locally
        ///     Useful for scenarios where you are replacing the global container
        ///     but needs to do some initializing that relies on it.
        /// </summary>
        /// <param name="localContainer"></param>
        /// <returns></returns>
        public static IDisposable UseLocalContainer(IWindsorContainer localContainer)
        {
            LocalContainerStack.Push(localContainer);
            return new DisposableAction(delegate { Reset(localContainer); });
        }

        public static void Reset(IWindsorContainer containerToReset)
        {
            if (containerToReset == null)
            {
                return;
            }

            if (ReferenceEquals(LocalContainer, containerToReset))
            {
                LocalContainerStack.Pop();
                if (LocalContainerStack.Count == 0)
                {
                    Local.Data[_localContainerKey] = null;
                }

                return;
            }

            if (ReferenceEquals(GlobalContainer, containerToReset))
            {
                GlobalContainer = null;
            }
        }

        public static void Reset()
        {
            var windsorContainer = LocalContainer ?? GlobalContainer;
            Reset(windsorContainer);
        }

        public static Array ResolveAll(Type service)
        {
            return Container.ResolveAll(service);
        }

        public static T[] ResolveAll<T>()
        {
            return Container.ResolveAll<T>();
        }
    }
}