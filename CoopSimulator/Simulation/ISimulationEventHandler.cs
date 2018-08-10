using CoopSimulator.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoopSimulator.Simulation
{
    /// <summary>
    /// Defines asynchronous methods to handle the Simulation timed events (processed animals).
    /// </summary>
    public interface ISimulationEventHandler
    {
        /// <summary>
        /// Asynchronously handles the operation of the animals which become available for mating.
        /// </summary>
        /// <param name="animals">Animals which are ready to become available for mating</param>
        /// <returns>Returns Task object which represents an asynchronous operation</returns>
        Task OnSexuallyMature(List<IAnimal> animals);

        /// <summary>
        /// Asynchronously handles the operation of the animals which become available for mating from unavailable.
        /// </summary>
        /// <param name="animals">Animals which are ready to become available for mating</param>
        /// <returns>Returns Task object which represents an asynchronous operation</returns>
        Task OnAvailable(List<IAnimal> animals);

        /// <summary>
        /// Asynchronously handles the operation of the animal couples which are matched for mating.
        /// </summary>
        /// <param name="tuples">The animal couples which are matched for mating</param>
        /// <returns>Returns Task object which represents an asynchronous operation/returns>
        Task OnMatingPartnersFound(List<Tuple<IFemale, IMale>> tuples);

        /// <summary>
        /// Asynchronously handles the operation of the females which are ready to give birth.
        /// </summary>
        /// <param name="females">The females which are ready to give birth</param>
        /// <returns>Returns Task object which represents an asynchronous operation</returns>
        Task OnBirth(List<IFemale> females);

        /// <summary>
        /// Asynchronously handles the operation when an animal dies.
        /// </summary>
        /// <param name="animal">Animal which dies</param>
        /// <returns>Returns Task object which represents an asynchronous operation</returns>
        Task OnDeath(IAnimal animal);
    }
}
