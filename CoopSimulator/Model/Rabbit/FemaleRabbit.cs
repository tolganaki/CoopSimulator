using System;
using System.Collections.Generic;

namespace CoopSimulator.Model.Rabbit
{
    public class FemaleRabbit : Rabbit, IFemale
    {
        public FemaleRabbit() : this(0)
        {

        }

        public FemaleRabbit(short age) : base(age)
        {

        }

        public List<IAnimal> GiveBirth()
        {
            var newBornList = new List<IAnimal>();
            int newBornCount = (int)Math.Round(Rabbit.NormalBirthCount.Sample());
            for (int i = 0; i < newBornCount; i++)
            {
                newBornList.Add(App.AnimalFactory.CreateAnimal());
            }
            return newBornList;
        }
    }
}
