using CoopSimulator.Model;
using CoopSimulator.Simulation.Events;
using CoopSimulator.Simulation.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoopSimulator.Simulation
{
    /// <summary>
    /// Handles the main simulation operation.
    /// Contains three concurrent collections which keep all animal instances during entire simulation process.
    /// </summary>
    public class Simulation : ISimulationEventHandler, IObserver<IEvent>
    {
        /// <summary>
        /// SimulationHelper instance
        /// </summary>
        private readonly SimulationHelper simulationHelper;

        /// <summary>
        /// Thread safe collection which keeps sexually available male animals.
        /// </summary>
        private readonly ConcurrentBag<IMale> availableMales;

        /// <summary>
        /// Thread safe collection which keeps sexually available female animals.
        /// </summary>
        private readonly ConcurrentBag<IFemale> availableFemales;

        /// <summary>
        /// Thread safe collection which keeps processed animals such as Pregnant or Unavailable etc..
        /// Key of the ConcurrentDictionary represents the process execution day.
        /// ConcurrentDictionary<PROCESS_EXECUTION_DAY, ConcurrentBag<ProcessedAnimal>>
        /// </summary>
        private readonly ConcurrentDictionary<int, ConcurrentBag<ProcessedAnimal>> processedAnimals;
        
        private readonly int totalSimulationDays;

        private int simulationDay = 0;
        private int deadAnimalCount = 0;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="config">Simulation configuration</param>
        /// <param name="intialAnimals">Provided animals which the simulation will start with</param>
        /// <param name="totalMonths">Total duration of the simulation in months</param>
        public Simulation(List<IAnimal> intialAnimals, int totalMonths)
        {
            availableMales = new ConcurrentBag<IMale>();
            availableFemales = new ConcurrentBag<IFemale>();
            processedAnimals = new ConcurrentDictionary<int, ConcurrentBag<ProcessedAnimal>>();
            
            simulationHelper = new SimulationHelper(this);
            
            // add initial animals to proper collections
            intialAnimals.ForEach(a =>
            {
                if (a.Age < a.SexualMaturatyAge) // animal is not sexually mature
                {
                    AddToProcessedAnimals(a, a.SexualMaturatyAge - a.Age, ProcessType.SexuallyImmature);
                }
                else
                {
                    if (a is IFemale)
                        availableFemales.Add(a as IFemale);
                    else
                        availableMales.Add(a as IMale);
                }

                // subscribe to animal istance to receive the generated events
                a.Subscribe(this);
            });

            totalSimulationDays = totalMonths * Constants.MONTH_DAYS;
        }

        private DateTime startDate;

        /// <summary>
        /// Starts the simulation process.
        /// </summary>
        public async void Start()
        {
            startDate = DateTime.Now;
            Console.WriteLine("Started: " + startDate.ToString(Constants.DEFAULT_DATE_FORMAT));

            while (simulationDay <= totalSimulationDays)
            {
                // handle processed animals
                if (processedAnimals.ContainsKey(simulationDay))
                {
                    if (processedAnimals.TryGetValue(simulationDay, out ConcurrentBag<ProcessedAnimal> currentBag))
                    {
                        await simulationHelper.HandleCurrentProcessedAnimals(currentBag);
                    }

                    // remove key/value pair of current simulationDay from processedAnimals if exists
                    processedAnimals.TryRemove(simulationDay, out ConcurrentBag<ProcessedAnimal> currentBagDummy);
                }

                // handle available animals
                if (availableMales.Count > 0 && availableFemales.Count > 0)
                    await simulationHelper.HandleAvailableAnimals(availableMales, availableFemales);

                if (simulationDay % Constants.MONTH_DAYS == 0)
                    WriteResultsToConsole();

                IncrementAges();
                simulationDay++;
            }

            DateTime endDate = DateTime.Now;
            Console.WriteLine("Ended: " + endDate.ToString(Constants.DEFAULT_DATE_FORMAT));
            Console.WriteLine("Total Seconds: " + endDate.Subtract(startDate).TotalSeconds);
        }

        #region private methods

        /// <summary>
        /// Increments the ages of the animals in the collections of availableMales and availableFemales by one.
        /// Age of the animals in processedAnimals collection is handled in AddToProcessedAnimals method.
        /// </summary>
        private void IncrementAges()
        {
            availableMales.ToList().ForEach(a =>
            {
                a.Age++;
            });

            availableFemales.ToList().ForEach(a =>
            {
                a.Age++;
            });
        }

        /// <summary>
        /// Helper method to add animal and the related process to processedAnimals collection.
        /// </summary>
        /// <param name="animal">Animal to process</param>
        /// <param name="duration">Duration of the process</param>
        /// <param name="processType">Type of the process</param>
        private void AddToProcessedAnimals(IAnimal animal, int duration, ProcessType processType)
        {
            animal.Age += (short)duration;
            processedAnimals.GetOrAdd(duration + simulationDay, new ConcurrentBag<ProcessedAnimal>())
                                    .Add(new ProcessedAnimal(animal, processType));
        }

        private void WriteResultsToConsole()
        {
            var totalCount = 0;
            var maleCount = 0;
            var femaleCount = 0;

            maleCount += availableMales.Count(m => m.IsAlive);
            femaleCount += availableFemales.Count(fm => fm.IsAlive);

            processedAnimals.Values.ToList().ForEach(cb =>
            {
                maleCount += cb.AsParallel().Count(pa => pa.Animal.IsAlive && pa.Animal is IMale);
                femaleCount += cb.AsParallel().Count(pa => pa.Animal.IsAlive && pa.Animal is IFemale);
            });

            totalCount += maleCount + femaleCount + deadAnimalCount;

            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString(Constants.DEFAULT_DATE_FORMAT) + " " + now.Subtract(startDate).TotalSeconds + "  Month: " + (simulationDay/ Constants.MONTH_DAYS) + " - Day: " + simulationDay + " - Total: " + totalCount + " Male: " + maleCount + " Female: " + femaleCount + " Dead: " + deadAnimalCount);
        }

        #endregion

        #region ISimulationEventHandler methods

        public Task OnSexuallyMature(List<IAnimal> animals)
        {
            return Task.Run(() =>
            {
                foreach (var animal in animals)
                {
                    if (animal is IMale)
                        availableMales.Add(animal as IMale);
                    else
                        availableFemales.Add(animal as IFemale);
                }
            });
        }

        public Task OnAvailable(List<IAnimal> animals)
        {
            return Task.Run(() =>
            {
                foreach (var animal in animals)
                {
                    if (animal is IMale)
                        availableMales.Add(animal as IMale);
                    else
                        availableFemales.Add(animal as IFemale);
                }
            });
        }

        public Task OnBirth(List<IFemale> females)
        {
            return Task.Run(() =>
            {
                foreach (var female in females)
                {
                    female.GiveBirth().ForEach(a =>
                    {
                        a.Subscribe(this);
                        AddToProcessedAnimals(a, a.SexualMaturatyAge - a.Age, ProcessType.SexuallyImmature);
                    });

                    AddToProcessedAnimals(female, App.SimulationConfig.AvailabilityAfterBirth, ProcessType.Unavailable);
                }
            });
        }

        public Task OnMatingPartnersFound(List<Tuple<IFemale, IMale>> tuples)
        {
            return Task.Run(() =>
            {
                foreach (var tuple in tuples)
                {
                    if (simulationHelper.Mate(tuple.Item1, tuple.Item2))
                    {
                        AddToProcessedAnimals(tuple.Item1, App.SimulationConfig.PregnancyLength, ProcessType.Pregnant);
                    }
                    else
                    {
                        AddToProcessedAnimals(tuple.Item1, App.SimulationConfig.AvailabilityAfterMating, ProcessType.Unavailable);
                    }

                    AddToProcessedAnimals(tuple.Item2, App.SimulationConfig.AvailabilityAfterMating, ProcessType.Unavailable);
                }
            });
        }

        public Task OnDeath(IAnimal animal)
        {
            Interlocked.Increment(ref deadAnimalCount);
            return Task.FromResult(0);
        }

        #endregion

        #region IObserver methods

        public void OnNext(IEvent value)
        {
            OnDeath(value.Animal);
        }

        public void OnError(Exception error)
        {
            // DO NOTHING
        }

        public void OnCompleted()
        {
            // DO NOTHING
        }

        #endregion
    }
}
