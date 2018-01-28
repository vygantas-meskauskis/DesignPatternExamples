using System;

namespace AbstractFactory
{
    public abstract class AbstractFactory
    {
        public static AbstractFactory GetFactory(Architecture architecture)
        {
            switch (architecture)
            {
                case Architecture.Enginola:
                    return new EmberFactory();
                case Architecture.Ember:
                    return new EnginolaFactory();
                default:
                    throw new ArgumentOutOfRangeException(nameof(architecture), architecture, null);
            }
        }

        public abstract Cpu CreateCpu();
        public abstract Mmu CreateMmu();
    }
}
