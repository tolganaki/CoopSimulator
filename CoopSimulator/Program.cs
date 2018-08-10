using CoopSimulator.Model;
using CoopSimulator.Simulation;
using System;
using System.Collections.Generic;

namespace CoopSimulator
{
    static class Program
    {
        static void Main(string[] args)
        {
            App.Initialize();

            while (true)
            {
                Console.Write("Please enter simulation cycles in months: ");
                var simulationCyclesString = Console.ReadLine();

                if (int.TryParse(simulationCyclesString, out int simulationCycles))
                {
                    var simulation = new Simulation.Simulation(new List<IAnimal>()
                    {
                        App.AnimalFactory.CreateMaleAnimal(10 * Constants.MONTH_DAYS),
                        App.AnimalFactory.CreateFemaleAnimal(10 * Constants.MONTH_DAYS)
                    }, simulationCycles);
                    simulation.Start();

                    Console.ReadLine();
                }
            }
        }
    }
}
