using CoopSimulator.Config;

namespace CoopSimulator.Model.Rabbit
{
    /// <summary>
    /// Represents a male Rabbit
    /// </summary>
    public class MaleRabbit : Rabbit, IMale
    {
        public MaleRabbit() : this(0)
        {

        }

        public MaleRabbit(short age): base (age)
        {
            
        }
    }
}
