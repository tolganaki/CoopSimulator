using CoopSimulator.Model;
using CoopSimulator.Model.Rabbit;
using MathNet.Numerics.Distributions;
using System;

namespace CoopSimulator.Factory.Rabbit
{
    /// <summary>
    /// Represent a singleton Rabbit Factory class which provides methods to create Rabbit instances.
    /// </summary>
    public class RabbitFactory : AnimalFactory
    {
        private static readonly Lazy<RabbitFactory> lazy = new Lazy<RabbitFactory>(() =>
        {
            return new RabbitFactory();
        });

        public static RabbitFactory Instance { get { return lazy.Value; } }

        private RabbitFactory()
        {
            // set Rabbit specific probability distibution methods
            Model.Rabbit.Rabbit.NormalLifeExpectancy = new Normal(App.SimulationConfig.AverageLifeExpectancy, App.SimulationConfig.LifeExpectancyStdDev);
            Model.Rabbit.Rabbit.NormalSexualMaturatyAge = new Normal(App.SimulationConfig.AverageSexualMaturatyAge, App.SimulationConfig.SexualMaturatyAgeStdDev);
            Model.Rabbit.Rabbit.NormalBirthCount = new Normal(App.SimulationConfig.AverageLitterCount, App.SimulationConfig.LitterStdDev);
        }

        public override IAnimal CreateAnimal()
        {
            IAnimal animal = null;
            var sex = GetMaleOrFemale();
            if (sex == AnimalFactory.Sex.Female)
            {
                animal = new FemaleRabbit();
            }
            else
            {
                animal = new MaleRabbit();
            }
            return animal;
        }

        public override IAnimal CreateFemaleAnimal(short age)
        {
            return new FemaleRabbit(age);
        }

        public override IAnimal CreateMaleAnimal(short age)
        {
            return new MaleRabbit(age);
        }
    }
}
