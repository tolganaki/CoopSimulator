using CoopSimulator.Model;

namespace CoopSimulator.Simulation.Events
{
    /// <summary>
    /// Represents Animal death event.
    /// </summary>
    public class AnimalDeathEvent : IEvent
    {
        public IAnimal Animal { get; }

        public AnimalDeathEvent(IAnimal animal)
        {
            Animal = animal;
        }
    }
}
