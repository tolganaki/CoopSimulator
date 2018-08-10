using CoopSimulator.Model;

namespace CoopSimulator.Factory
{
    /// <summary>
    /// Represents a factory interface which provides methods to create Animal instances.
    /// </summary>
    public interface IAnimalFactory
    {
        IAnimal CreateAnimal();
        IAnimal CreateMaleAnimal(short age);
        IAnimal CreateFemaleAnimal(short age);
    }
}