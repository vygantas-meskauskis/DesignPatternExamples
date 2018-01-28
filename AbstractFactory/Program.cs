using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AbstractFactory
{
    public class Program
    {
        static void Main(string[] args)
        {
            var carName = "mini";
            new DemoWhenToConsiderFactoryPattern(carName);
            new SimpleFactoryPattern(carName);
            new FactoryMethodPattern(carName);
        }

    }

    #region Demo when to use factory pattern
    public class DemoWhenToConsiderFactoryPattern
    {
        public DemoWhenToConsiderFactoryPattern(string carName)
        {
            var name = GetCar(carName);
        }
        private static IAuto GetCar(string carName)
        {
            switch (carName)
            {
                case "bmw":
                    return new Bmw335Xi();
                case "mini":
                    return new Mini();
                // if we want to add a new car like Audi,
                // we violate Open/Closed solid principle. Class should be open for extension closed
                // for midification. In this case we modify class in order to add new car.

                //case "audi":
                //    return new Audi();
                default:
                    return null;
            }
        }
}
    #endregion

    #region SimpleFactory
    /*
Simple Factory
    -Encapsulate object creation
    -Allows for late-bound decisions regarding instantiation
        - configuration based
        - other persistance storage
        - input or other dynamic data
    -Caller class knows what concrete factory it needs
 */

    public class SimpleFactoryPattern
    {
        public SimpleFactoryPattern(string carName)
        {
            AutoFactory factory = new AutoFactory();

            IAuto car = factory.CreateInstance(carName);

            car.TurnOff();
            car.TurnOn();
        }
    }

    public class AutoFactory
    {
        Dictionary<string, Type> autos;

        public AutoFactory()
        {
            LoadTypesICanReturn();
        }

        public IAuto CreateInstance(string carName)
        {
            var type = GetTypeToReturn(carName);

            if (type == null)
                return null;

            return Activator.CreateInstance(type) as IAuto;
        }

        private Type GetTypeToReturn(string carName)
        {
            foreach (var auto in autos)
            {
                if (auto.Key.Contains(carName))
                {
                    return autos[auto.Key];
                }
            }

            return null;
        }

        void LoadTypesICanReturn()
        {
            autos = new Dictionary<string, Type>();

            var typesInThisAssembly = Assembly.GetEntryAssembly().GetTypes();

            foreach (var type in typesInThisAssembly)
            {
                // TODO fix this
                if (type.GetInterfaces().Any(i => i.GetType() == typeof(IAuto)))
                {
                    autos.Add(type.Name.ToLower(), type);
                }
            }
        }
    }
    #endregion

    #region Factory method
    
    /*
     Define an interface for creating an object but let the subclasses decide
     which class defer instantiation to subclasses

     - Adds an interface to the factory itself from SimpleFactory
     - Defer object creation to multiple factories that share an interface
     - Derived classes implement or override the factory method of the base class

     Advantage:
        Eliminates reference to concrete classes
            Factories
            Objects created by factories
        Factories can be inherited to provide even more specializer object creation (for example
        inherit more advanced car which adds packages etc.)
        Rules for object initalization is centralized

     Disantvantage
        May need to create factory just to get a concrete class derived
        The inheritance hierarchy gets deeper with coupling between concrete factories and created classes
     */ 

    class FactoryMethodPattern
    {
        public FactoryMethodPattern(string carName)
        {
            IAutoFactory factory = LoadFactories();

            IAuto car = factory.CreateAutomobile();

            car.TurnOff();
            car.TurnOn();
        }

        private IAutoFactory LoadFactories()
        {
            // todo move to configuration
            var factoryName = "MiniCooperFactory";

            return Assembly.GetEntryAssembly().CreateInstance(factoryName) as IAutoFactory;
        }
    }

    interface IAutoFactory
    {
        IAuto CreateAutomobile();
    }

    class MiniCooperFactory : IAutoFactory
    {
        public IAuto CreateAutomobile()
        {
            var mini = new Mini();
            mini.SetName("mini cooper s");

            return mini;
        }
    }

    #endregion


    #region Abstract Factory



    #endregion

    internal class Mini : IAuto
    {
        public string Name { get; }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public void TurnOff()
        {
            throw new System.NotImplementedException();
        }

        public void TurnOn()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class Bmw335Xi : IAuto
    {
        // this class has constructor therefore we dont need to call setname like in mini.cs
        public Bmw335Xi(string name)
        {
            
        }
        public string Name { get; }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public void TurnOff()
        {
            throw new System.NotImplementedException();
        }

        public void TurnOn()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IAuto
    {
        string Name { get; }
        void SetName(string name);
        void TurnOff();
        void TurnOn();
    }
}