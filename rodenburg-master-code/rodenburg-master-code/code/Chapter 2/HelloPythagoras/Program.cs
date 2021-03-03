using System;

namespace HelloPythagoras
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloPythagoras program = new HelloPythagoras();
            double squaredLength = program.Pythagoras(3F, 4F);

            Console.WriteLine(squaredLength);
            Console.ReadKey();
        }
    }
}
