using CoopSimulator.Simulation.Events;
using System;

namespace CoopSimulator.Model
{
    /// <summary>
    /// Represents a generic Animal.
    /// Implements Systme.IObservable<T> interface to publish events such as to inform animal death etc..
    /// </summary>
    public interface IAnimal : IObservable<IEvent>
    {
        /// <summary>
        /// Current age of the animal (in days).
        /// </summary>
        short Age { get; set; }

        /// <summary>
        /// Life span of the animal (in days).
        /// </summary>
        short LifeTime { get; set; }

        /// <summary>
        /// Sexual maturaty age of the animal (in days).
        /// </summary>
        short SexualMaturatyAge { get; set; }

        /// <summary>
        /// Determines if the animal is dead or alive.
        /// </summary>
        bool IsAlive { get; }
    }
}
