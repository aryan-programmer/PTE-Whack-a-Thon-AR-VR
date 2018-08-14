using System.Collections;

namespace Utilities.Timers
{
    /// <summary>
    /// Standard Timer Implementation Interface
    /// </summary>
    /// <typeparam name="T">
    /// Pass in the value returned by the start function, idealy the timer itself
    /// </typeparam>
    interface ITimer<T>
    {
        /// <summary>
        /// Starts the timer and idealy returns the timer
        /// </summary>
        /// <returns>
        /// Idealy returns the timer
        /// </returns>
        T Start ();

        /// <summary>
        /// Stops The Timer
        /// </summary>
        void Stop ();

		void OnUpdate( );

	}
}