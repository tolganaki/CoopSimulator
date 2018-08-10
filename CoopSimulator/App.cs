using CoopSimulator.Config;
using CoopSimulator.Factory;
using System.Configuration;

namespace CoopSimulator
{
    /// <summary>
    /// Static class which holds SimulationConfig and IAnimalFactory instances for the simulation modules to use
    /// </summary>
    public static class App
    {
        /// <summary>
        /// Holds a pointer of SimulationConfig instance for the entire simulation life cycle
        /// </summary>
        public static SimulationConfig SimulationConfig { get; private set; }

        /// <summary>
        /// Holds a pointer of AnimalFactory instance for the entire simulation life cycle
        /// </summary>
        public static IAnimalFactory AnimalFactory { get; private set; }
        
        public static void Initialize()
        {
            App.SimulationConfig = ConfigurationManager.GetSection("simulationConfig") as SimulationConfig;
            App.AnimalFactory = SimulationFactory.GetAnimalFactory(App.SimulationConfig);
        }
    }
}
