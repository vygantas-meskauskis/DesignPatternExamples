using System;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            var sandwichMaker = new SandwichMaker(new TastySandwichBuilder());
            sandwichMaker.BuildSandwich();
            var sandwich = sandwichMaker.GetSandwich();
        }
    }

    internal class SandwichMaker
    {
        private readonly SandwichBuilder _builder;

        public SandwichMaker(SandwichBuilder builder)
        {
            _builder = builder;
        }

        public void BuildSandwich()
        {
            _builder.CreateSandwich();
            _builder.PrepareBread();
            _builder.AddCheese();
        }

        public Sandwich GetSandwich()
        {
            return _builder.GetSandwich();
        }
    }

    internal abstract class SandwichBuilder
    {
        internal Sandwich Sandwich;

        public Sandwich GetSandwich()
        {
            return Sandwich;
        }

        public void CreateSandwich()
        {
            Sandwich = new Sandwich();
        }

        public abstract void PrepareBread();
        public abstract void AddCheese();
    }

    internal class Sandwich
    {
        public string Bread { get; set; }
        public string Cheese { get; set; }
        //............ and more stuff
    }

    internal class TastySandwichBuilder : SandwichBuilder
    {
        public override void PrepareBread()
        {
            Sandwich.Bread = "breadtype";
        }

        public override void AddCheese()
        {
            Sandwich.Cheese = "TastyCheese";
        }
    }
}