using MathNet.Numerics.Distributions;
using System;

namespace CoopSimulator.Model.Rabbit
{
    /// <summary>
    /// Represents a generic Rabbit which provides common functionalities for the male and female Rabbit as a base class.
    /// </summary>
    public abstract class Rabbit : Animal
    {
        public static Normal NormalLifeExpectancy { get; set; }
        public static Normal NormalSexualMaturatyAge { get; set; }
        public static Normal NormalBirthCount  { get; set; }

        protected Rabbit() : this(0)
        {

        }

        protected Rabbit(short age)
        {
            PopulateLifeTime();
            PopulateSexualMaturatyAge();

            Age = age;
        }

        /// <summary>
        /// Default implementation to populate life time of a Rabbit.
        /// </summary>
        protected override void PopulateLifeTime()
        {
            LifeTime = (short)Math.Round(Rabbit.NormalLifeExpectancy.Sample());
        }

        /// <summary>
        /// Default implementation to populate sexual maturaty age of a Rabbit.
        /// </summary>
        protected override void PopulateSexualMaturatyAge()
        {
            SexualMaturatyAge = (short)Math.Round(Rabbit.NormalSexualMaturatyAge.Sample());
        }
    }
}
