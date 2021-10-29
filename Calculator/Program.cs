using System;
using System.Data;

namespace CalculatorNS
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;

            do
            {
                Console.Write("Enter: ");
                input = Console.ReadLine();

                decimal result = Calculator.Calculate(input);

                Console.WriteLine("Sum is: {0}", result.ToString());
            } while(true);
        }
    }
}
