using CoopSimulator.Config;
using CoopSimulator.Factory.Rabbit;

namespace CoopSimulator.Factory
{
    /// <summary>
    /// Global simulation factory class
    /// </summary>
    public static class SimulationFactory
    {
        /// <summary>
        /// Provides animal factory instance for the given simulation configuration.
        /// </summary>
        /// <param name="config">Simulation configuration</param>
        /// <returns>Returns animal factory</returns>
        public static IAnimalFactory GetAnimalFactory(SimulationConfig config)
        {
            if (config.AnimalType == SimulationConfig.SimulationAnimalType.Rabbit)
            {
                return RabbitFactory.Instance;
            }
            return null;
        }
    }
}
