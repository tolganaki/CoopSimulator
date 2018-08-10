using CoopSimulator.Model;
using System;
using System.Threading;

namespace CoopSimulator.Factory
{
    /// <summary>
    /// Base animal factory class.
    /// </summary>
    public abstract class AnimalFactory : IAnimalFactory
    {
        /// <summary>
        /// Random number generator, sepeately handled in each thread.
        /// </summary>
        protected readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

        /// <summary>
        /// Represents the sex of the animal.
        /// </summary>
        protected enum Sex
        {
            Female,
            Male
        }

        /// <summary>
        /// Decides the sex of the newly created animal.
        /// </summary>
        /// <returns>Returns the sex of the newly created animal</returns>
        protected Sex GetMaleOrFemale()
        {
            return random.Value.Next(0, 2) == 0 ? Sex.Female : Sex.Male;
        }

        public abstract IAnimal CreateAnimal();

        public abstract IAnimal CreateMaleAnimal(short age);

        public abstract IAnimal CreateFemaleAnimal(short age);
    }
}
