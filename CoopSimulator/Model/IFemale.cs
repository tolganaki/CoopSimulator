using System.Collections.Generic;

namespace CoopSimulator.Model
{
    /// <summary>
    /// Represent a female Animal.
    /// </summary>
    public interface IFemale : IAnimal
    {
        /// <summary>
        /// Female gives birth to new babies.
        /// </summary>
        /// <returns>Returns the new born animals list</returns>
        List<IAnimal> GiveBirth();
    }
}
