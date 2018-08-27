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
        /// Provides animal factory instance from the current simulation configuration.
        /// </summary>
        /// <returns>Returns animal factory</returns>
        public static IAnimalFactory GetAnimalFactory()
        {
            if (App.SimulationConfig.AnimalType == SimulationConfig.SimulationAnimalType.Rabbit)
            {
                return RabbitFactory.Instance;
            }
            return null;
        }
    }
}
