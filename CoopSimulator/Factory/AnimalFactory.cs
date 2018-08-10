using CoopSimulator.Model;
using System;
using System.Threading;

namespace CoopSimulator.Factory
{
    /// <summary>
    /// Base animal factory class which implements IAnimalFactory.
    /// </summary>
    public abstract class AnimalFactory : IAnimalFactory
    {
        protected readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

        protected enum Sex
        {
            Female,
            Male
        }

        protected Sex GetMaleOrFemale()
        {
            return random.Value.Next(0, 2) == 0 ? Sex.Female : Sex.Male;
        }

        public abstract IAnimal CreateAnimal();

        public abstract IAnimal CreateMaleAnimal(short age);

        public abstract IAnimal CreateFemaleAnimal(short age);
    }
}
