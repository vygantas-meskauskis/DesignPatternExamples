using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            var standartFormatter = new StandartFormatter();
            var backwardsFormatter = new BackWardsFormatter();

            var documents = new List<MenuScript>();

            var book = new Book(standartFormatter);
            var paper = new Paper(backwardsFormatter);

            documents.Add(book);
            documents.Add(paper);

            foreach (var menuScript in documents)
            {
                menuScript.Print();
            }
            
        }
    }

    public abstract class MenuScript
    {
        protected  readonly IMenuFormatter Formatter;
        public MenuScript(IMenuFormatter formatter)
        {
            Formatter = formatter;
        }
        public abstract void Print();
    }

    public class Paper : MenuScript
    {
        public string Title { get; set; }
        public Paper(IMenuFormatter backwardsFormatter) : base(backwardsFormatter)
        {
            Console.WriteLine(Title);
        }

        public override void Print()
        {
            Console.WriteLine();
        }
    }

    public class Book : MenuScript
    {
        public string BookTitle { get; set; }
        public Book(IMenuFormatter standartFormatter) : base(standartFormatter)
        {
        }

        public override void Print()
        {
            Console.WriteLine(BookTitle);
        }
    }

    public class BackWardsFormatter : IMenuFormatter
    {
    }

    public class StandartFormatter : IMenuFormatter
    {
    }

    public interface IMenuFormatter
    {
        
    }
}
