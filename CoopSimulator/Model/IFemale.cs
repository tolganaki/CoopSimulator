using System.Collections.Generic;

namespace CoopSimulator.Model
{
    /// <summary>
    /// Represent a female Animal.
    /// </summary>
    public interface IFemale : IAnimal
    {
        List<IAnimal> GiveBirth();
    }
}
