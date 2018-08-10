using CoopSimulator.Config;
using CoopSimulator.Factory.Rabbit;

namespace CoopSimulator.Factory
{
    public static class SimulationFactory
    {
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
