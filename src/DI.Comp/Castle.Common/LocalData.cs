using System;
using System.Collections;

namespace Castle.Common
{
    public interface ILocalData
    {
        /// <summary>
        ///     Gets or sets the <see cref="System.Object" /> with the specified key.
        /// </summary>
        /// <value></value>
        object this[object key] { get; set; }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        void Clear();
    }

    public static class Local
    {
        /// <summary>
        ///     Gets the current data
        /// </summary>
        /// <value>The data.</value>
        public static ILocalData Data { get; } = new LocalData();

        private class LocalData : ILocalData
        {
            [ThreadStatic] private static Hashtable _threadHashtable;

            private static Hashtable LocalHashtable => _threadHashtable ??
                                                       (
                                                           _threadHashtable = new Hashtable()
                                                       );

            public object this[object key]
            {
                get => LocalHashtable[key];
                set => LocalHashtable[key] = value;
            }

            public void Clear()
            {
                LocalHashtable.Clear();
            }
        }
    }
}