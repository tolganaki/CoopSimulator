using System.Configuration;

namespace CoopSimulator.Config
{
    /// <summary>
    /// Represents simulation configuration, will be automatically initialized by System.Configuration.
    /// </summary>
    public class SimulationConfig : ConfigurationSection
    {
        public enum SimulationAnimalType
        {
            Rabbit
        }

        /// <summary>
        /// Animal type.
        /// </summary>
        [ConfigurationProperty("animalType", IsRequired = true, DefaultValue = SimulationAnimalType.Rabbit)]
        public SimulationAnimalType AnimalType
        {
            get
            {
                return (SimulationAnimalType)this["animalType"];
            }
            set
            {
                this["animalType"] = value;
            }
        }

        /// <summary>
        /// Number of operations will be given to an asynchronous operation (Task)
        /// </summary>
        [ConfigurationProperty("simulationOperationCountPerTask", IsRequired = true)]
        public int OperationCountPerTask
        {
            get
            {
                return (int)this["simulationOperationCountPerTask"];
            }
            set
            {
                this["simulationOperationCountPerTask"] = value;
            }
        }

        /// <summary>
        /// Average sexual maturaty age of the animal (in days).
        /// </summary>
        [ConfigurationProperty("averageSexualMaturatyAge", IsRequired = true)]
        public double AverageSexualMaturatyAge
        {
            get
            {
                return (double)this["averageSexualMaturatyAge"];
            }
            set
            {
                this["averageSexualMaturatyAge"] = value;
            }
        }

        /// <summary>
        /// Sexual maturaty age standard deviation value.
        /// This value will be used for normalizing an animal's sexual maturaty age.
        /// </summary>
        [ConfigurationProperty("sexualMaturatyAgeStdDev", IsRequired = true)]
        public double SexualMaturatyAgeStdDev
        {
            get
            {
                return (double)this["sexualMaturatyAgeStdDev"];
            }
            set
            {
                this["sexualMaturatyAgeStdDev"] = value;
            }
        }

        /// <summary>
        /// Pregnancy length of the female (in days).
        /// </summary>
        [ConfigurationProperty("pregnancyLength", IsRequired = true)]
        public short PregnancyLength
        {
            get
            {
                return (short)this["pregnancyLength"];
            }
            set
            {
                this["pregnancyLength"] = value;
            }
        }

        /// <summary>
        /// The number of days that the female will be sexually available after giving birth.
        /// </summary>
        [ConfigurationProperty("availabilityAfterBirth", IsRequired = true)]
        public short AvailabilityAfterBirth
        {
            get
            {
                return (short)this["availabilityAfterBirth"];
            }
            set
            {
                this["availabilityAfterBirth"] = value;
            }
        }

        /// <summary>
        /// The number of days that the animal will be sexually available after mating.
        /// </summary>
        [ConfigurationProperty("availabilityAfterMating", IsRequired = true)]
        public short AvailabilityAfterMating
        {
            get
            {
                return (short)this["availabilityAfterMating"];
            }
            set
            {
                this["availabilityAfterMating"] = value;
            }
        }

        /// <summary>
        /// Average litter count of the female after the birth.
        /// </summary>
        [ConfigurationProperty("averageLitterCount", IsRequired = true)]
        public double AverageLitterCount
        {
            get
            {
                return (double)this["averageLitterCount"];
            }
            set
            {
                this["averageLitterCount"] = value;
            }
        }

        /// <summary>
        /// Litter count standard deviation value.
        /// </summary>
        [ConfigurationProperty("litterStdDev", IsRequired = true)]
        public double LitterStdDev
        {
            get
            {
                return (double)this["litterStdDev"];
            }
            set
            {
                this["litterStdDev"] = value;
            }
        }

        /// <summary>
        /// Average life expectancy of the animal.
        /// </summary>
        [ConfigurationProperty("averageLifeExpectancy", IsRequired = true)]
        public double AverageLifeExpectancy
        {
            get
            {
                return (double)this["averageLifeExpectancy"];
            }
            set
            {
                this["averageLifeExpectancy"] = value;
            }
        }

        /// <summary>
        /// Life expectancy standard deviation value.
        /// </summary>
        [ConfigurationProperty("lifeExpectancyStdDev", IsRequired = true)]
        public double LifeExpectancyStdDev
        {
            get
            {
                return (double)this["lifeExpectancyStdDev"];
            }
            set
            {
                this["lifeExpectancyStdDev"] = value;
            }
        }
    }
}
