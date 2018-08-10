using CoopSimulator.Factory;
using CoopSimulator.Simulation.Events;
using System;

namespace CoopSimulator.Model
{
    /// <summary>
    /// Represents a generic Animal which implements IAnimal interface.
    /// Provides basic properties of an Animal such as Age, Life Time and Sexual Maturaty age etc..
    /// Life Time and Sexual Maturaty Age should be pre-calculated at construction phase of the derived class.
    /// </summary>
    public abstract class Animal : IAnimal
    {
        private IObserver<IEvent> observer;
        
        private short age;
        public short Age
        {
            get
            {
                return age;
            }
            set
            {
                if (!IsAlive)
                    return;

                age = value;
                if (age > LifeTime)
                {
                    age = LifeTime;
                    IsAlive = false;
                    if (observer != null)
                    {
                        observer.OnNext(new AnimalDeathEvent(this));
                    }
                }
            }
        }

        public short LifeTime { get; set; }
        public short SexualMaturatyAge { get; set; }
        public bool IsAlive { get; private set; } = true;

        /// <summary>
        /// Derived class should override this class to pre-calculate the Life Time of this animal and set at initialization.
        /// </summary>
        protected abstract void PopulateLifeTime();

        /// <summary>
        /// Derived class should override this class to pre-calculate the Sexual Maturaty Age of this animal and set at initialization.
        /// </summary>
        protected abstract void PopulateSexualMaturatyAge();

        /// <summary>
        /// Subscribes the source class (observer) to receive Events from this class.
        /// </summary>
        /// <param name="observer">The observer class which subscribes</param>
        /// <returns>Returns null</returns>
        public IDisposable Subscribe(IObserver<IEvent> observer)
        {
            this.observer = observer;
            return null;
        }
    }
}
