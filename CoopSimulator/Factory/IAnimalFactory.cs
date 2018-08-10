using CoopSimulator.Model;

namespace CoopSimulator.Factory
{
    /// <summary>
    /// Represents a factory interface which provides methods to create Animal instances.
    /// </summary>
    public interface IAnimalFactory
    {
        /// <summary>
        /// Creates a new animal.
        /// </summary>
        /// <returns>Returns the new animal</returns>
        IAnimal CreateAnimal();

        /// <summary>
        /// Creates a new male animal.
        /// </summary>
        /// <param name="age">Initial age of the male animal</param>
        /// <returns>Returns the new male animal</returns>
        IAnimal CreateMaleAnimal(short age);

        /// <summary>
        /// Creates a new female animal.
        /// </summary>
        /// <param name="age">Initial age of the female animal</param>
        /// <returns>Returns the new female animal</returns>
        IAnimal CreateFemaleAnimal(short age);
    }
}