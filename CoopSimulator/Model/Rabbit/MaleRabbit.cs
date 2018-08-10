using CoopSimulator.Config;

namespace CoopSimulator.Model.Rabbit
{
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
