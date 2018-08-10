using CoopSimulator.Model;

namespace CoopSimulator.Simulation.Events
{
    /// <summary>
    /// Represents a simulation event.
    /// </summary>
    public interface IEvent
    {
        IAnimal Animal { get; set; }
    }
}