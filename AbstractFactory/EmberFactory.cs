namespace AbstractFactory
{
    public class EmberFactory : AbstractFactory
    {
        public override Cpu CreateCpu()
        {
            return new EmberCpu();
        }

        public override Mmu CreateMmu()
        {
            return new EmberMmu();
        }
    }
}