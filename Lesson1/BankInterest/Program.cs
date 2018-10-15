using System;
using System.Globalization;

namespace BankInterest
{
    class Program
    {
        static void Main()
        {
            var input = Console.ReadLine();
            Console.WriteLine(Calculate(input));
        }

        public static double Calculate(string userInput)
        {
            var parameters = userInput.Split();

            var deposit = double.Parse(parameters[0], CultureInfo.InvariantCulture);
            var interestRate = double.Parse(parameters[1], CultureInfo.InvariantCulture) / 100.0;
            var months = int.Parse(parameters[2]);

            return deposit * Math.Pow(1 + interestRate / 12, months);
        }
    }
}
