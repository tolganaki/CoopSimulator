using CoopSimulator.Model;

namespace CoopSimulator.Simulation.Model
{
    /// <summary>
    /// Represents a container which holds the Animal instance and the related Process.
    /// </summary>
    public struct ProcessedAnimal
    {
        public IAnimal Animal { get; }
        public ProcessType ProcessType { get; }

        public ProcessedAnimal(IAnimal animal, ProcessType processType)
        {
            this.Animal = animal;
            this.ProcessType = processType;
        }
    }

    /// <summary>
    /// Type of the process which the Animal is related to.
    /// </summary>
    public enum ProcessType
    {
        /// <summary>
        /// Represents the state of an animal which is in the process of becoming sexually mature from sexually immature
        /// </summary>
        SexuallyImmature,

        /// <summary>
        /// Represents the state of an animal which is in the process of becoming sexually available from sexually unavailable
        /// </summary>
        Unavailable,

        /// <summary>
        /// Represents the state of an female animal which is in the process of giving birth from pregnant
        /// </summary>
        Pregnant
    }
}