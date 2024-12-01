using System;

namespace Castle.Common
{
    public class DisposableAction<T> : IDisposable
    {
        private readonly Action<T> _action;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisposableAction&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="action">The action to execute on dispose</param>
        /// <param name="val">The value that will be passed to the action on dispose</param>
        public DisposableAction(Action<T> action, T val)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            Value = val;
        }


        /// <summary>
        ///     Gets the value associated with this action
        /// </summary>
        /// <value>The value.</value>
        public T Value { get; }


        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _action(Value);
        }
    }


    /// <summary>
    ///     Better sytnax for context operation.
    ///     Wraps a delegate that is executed when the Dispose method is called.
    ///     This allows to do context sensitive things easily.
    ///     Basically, it mimics Java's anonymous classes.
    /// </summary>
    public class DisposableAction : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisposableAction" /> class.
        /// </summary>
        /// <param name="action">The action to execute on dispose</param>
        public DisposableAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _action();
        }
    }
}