namespace AbstractFactory
{
    public class EnginolaFactory : AbstractFactory
    {
        public override Cpu CreateCpu()
        {
            return new EnginolaCpu();
        }

        public override Mmu CreateMmu()
        {
            return new EnginolaMmu();
        }
    }
}