namespace VirtualParadise.Examples
{
    #region Using Directives

    using System;

    #endregion

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Fluent API example:");
            FluentExample.RunExample();

            Console.WriteLine("\nSequencing example:");
            SequencingExample.RunExample();

            Console.WriteLine("\nParsing example:");
            ParsingExample.RunExample();

            Console.ReadLine();
        }
    }
}
