using System;

using Castle.MicroKernel.Facilities;

namespace Castle.Common.Facilities.EventWiring
{
    /// <summary>
    ///     Exception that is thrown when a error occurs during the Event Wiring process
    /// </summary>
    [Serializable]
    public class EventWiringException : FacilityException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EventWiringException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EventWiringException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventWiringException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public EventWiringException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}