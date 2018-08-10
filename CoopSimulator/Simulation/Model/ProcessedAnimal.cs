using CoopSimulator.Model;

namespace CoopSimulator.Simulation.Model
{
    /// <summary>
    /// Represents a container which holds the Animal instance and the related Process.
    /// </summary>
    public struct ProcessedAnimal
    {
        public IAnimal Animal { get; set; }
        public ProcessType ProcessType { get; set; }

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
        SexuallyImmature,
        Unavailable,
        Pregnant
    }
}