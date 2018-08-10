using CoopSimulator.Model;
using CoopSimulator.Simulation.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoopSimulator.Simulation
{
    /// <summary>
    /// Helper class which provides methods to handle particular simulation operations.
    /// </summary>
    public class SimulationHelper
    {
        private readonly ThreadLocal<Random> random;
        private readonly ISimulationEventHandler simulationEventHandler;

        public SimulationHelper(ISimulationEventHandler simulationEventHandler)
        {
            random = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
            this.simulationEventHandler = simulationEventHandler;
        }

        /// <summary>
        /// Handles animal processes which are intended to happen only in this current cycle.
        /// </summary>
        /// <param name="currentBag">Collection of Animal processes, targeted to execute at current cycle of the simulation</param>
        /// <returns>Returns Task object which represents an asynchronous operation</returns>
        public async Task HandleCurrentProcessedAnimals(ConcurrentBag<ProcessedAnimal> currentBag)
        {
            var tasks = new List<Task>();
            var sexuallyImmatureList = new List<IAnimal>();
            var unavailableList = new List<IAnimal>();
            var pregnantList = new List<IFemale>();

            ProcessedAnimal processedAnimal;
            while (currentBag.TryTake(out processedAnimal))
            {
                if (processedAnimal.Animal.IsAlive) // check for alive animals, dead animals will be removed from simulation collections.
                {
                    if (processedAnimal.ProcessType == ProcessType.SexuallyImmature)
                        sexuallyImmatureList.Add(processedAnimal.Animal);
                    else if (processedAnimal.ProcessType == ProcessType.Unavailable)
                        unavailableList.Add(processedAnimal.Animal);
                    else if (processedAnimal.ProcessType == ProcessType.Pregnant)
                        pregnantList.Add(processedAnimal.Animal as IFemale);
                }
            }

            // handles the list of "sexually immature animals to become sexually mature" by grouping
            // predefined number of animals and routing them to be handled by a new asynchronous Task
            for (int i = 0; i < sexuallyImmatureList.Count; i += App.SimulationConfig.OperationCountPerTask)
            {
                tasks.Add(HandleSexuallyImmatureAnimals(sexuallyImmatureList.GetRange(i, Math.Min(App.SimulationConfig.OperationCountPerTask, sexuallyImmatureList.Count - i))));
            }

            // handles the list of "unavailable animals to become available" by grouping
            // predefined number of animals and routing them to be handled by a new asynchronous Task 
            for (int i = 0; i < unavailableList.Count; i += App.SimulationConfig.OperationCountPerTask)
            {
                tasks.Add(HandleUnavailableAnimals(unavailableList.GetRange(i, Math.Min(App.SimulationConfig.OperationCountPerTask, unavailableList.Count - i))));
            }

            // handles the list of "pregnant animals to give birth" by grouping 
            // predefined number of females and routing them to be handled by a new asynchronous Task 
            for (int i = 0; i < pregnantList.Count; i += App.SimulationConfig.OperationCountPerTask)
            {
                tasks.Add(HandlePregnantAnimals(pregnantList.GetRange(i, Math.Min(App.SimulationConfig.OperationCountPerTask, pregnantList.Count - i))));
            }

            // Waits for all the asynchronous tasks to be completed in order to proceed.
            // This is important for the simulation execution in order not to bypass any operation which may cause wrong simulation results.
            await Task.WhenAll(tasks.ToArray());
        }

        private Task HandleSexuallyImmatureAnimals(List<IAnimal> animals)
        {
            return simulationEventHandler.OnSexuallyMature(animals);
        }

        private Task HandleUnavailableAnimals(List<IAnimal> animals)
        {
            return simulationEventHandler.OnAvailable(animals);
        }

        private Task HandlePregnantAnimals(List<IFemale> females)
        {
            return simulationEventHandler.OnBirth(females);
        }

        /// <summary>
        /// Tries to finds all possible mating partners in all available animals.
        /// When a male and female is matched, they are removed from the collections of available animals.
        /// </summary>
        /// <param name="availableMales">Collection which keeps available males</param>
        /// <param name="availableFemales">Collection which keeps available females</param>
        /// <returns>Returns Task object which represents an asynchronous operation</returns>
        public async Task HandleAvailableAnimals(ConcurrentBag<IMale> availableMales, ConcurrentBag<IFemale> availableFemales)
        {
            var tasks = new List<Task>();
            var tuples = new List<Tuple<IFemale, IMale>>();

            IFemale female = null;
            IMale male = null;
            while (availableFemales.TryTake(out female))
            {
                if (!female.IsAlive) // check for alive females, dead animals will be removed from simulation collections.
                    continue;

                male = null;
                while (availableMales.TryTake(out male))
                {
                    if (!male.IsAlive) // check for alive males, dead animals will be removed from simulation collections.
                        continue;

                    tuples.Add(new Tuple<IFemale, IMale>(female, male));
                    break;
                }

                if (male == null)
                {
                    // add female back to available females
                    availableFemales.Add(female);
                    break;
                }
            }

            // handles the list of "male and female couples to mate" by grouping 
            // predefined number of the couples together and routing them to be handled by a new asynchronous Task 
            for (int i = 0; i < tuples.Count; i += App.SimulationConfig.OperationCountPerTask)
            {
                tasks.Add(simulationEventHandler.OnMatingPartnersFound(tuples.GetRange(i, Math.Min(App.SimulationConfig.OperationCountPerTask, tuples.Count - i))));
            }

            // Waits for all the asynchronous tasks to be completed in order to proceed.
            // This is important for the simulation execution in order not to bypass any operation which may cause wrong simulation results.
            await Task.WhenAll(tasks.ToArray());
        }

        /// <summary>
        /// Determines if the mating will succeed with result of female pregnancy.
        /// Current implementation provides 50% pregnancy chance.
        /// </summary>
        /// <param name="female">female to mate</param>
        /// <param name="male">male to mate</param>
        /// <returns>Returns true if female gets pregnant otherwise returns false</returns>
        public bool Mate(IFemale female, IMale male)
        {
            // Future Scope:
            //      Fertality rates can be added to male and female through configuration for pregnancy probability.
            return random.Value.Next(0, 2) == 0;
        }
    }
}
