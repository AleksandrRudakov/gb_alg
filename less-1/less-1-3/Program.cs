using System;

namespace less_1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Число Фибоначчи через цикл:");
            Console.WriteLine($"- F(0) = {FibonacciViaCycle(0)}");
            Console.WriteLine($"- F(1) = {FibonacciViaCycle(1)}");
            Console.WriteLine($"- F(2) = {FibonacciViaCycle(2)}");
            Console.WriteLine($"- F(3) = {FibonacciViaCycle(3)}");
            Console.WriteLine($"- F(4) = {FibonacciViaCycle(4)}");
            Console.WriteLine($"- F(5) = {FibonacciViaCycle(5)}");
            Console.WriteLine($"- F(6) = {FibonacciViaCycle(6)}");
            Console.WriteLine($"- F(7) = {FibonacciViaCycle(7)}");
            Console.WriteLine($"- F(8) = {FibonacciViaCycle(8)}");

            Console.WriteLine("Число Фибоначчи через рекурсию:");
            Console.WriteLine($"- F(0) = {FibonacciViaRecursion(0)}");
            Console.WriteLine($"- F(1) = {FibonacciViaRecursion(1)}");
            Console.WriteLine($"- F(2) = {FibonacciViaRecursion(2)}");
            Console.WriteLine($"- F(3) = {FibonacciViaRecursion(3)}");
            Console.WriteLine($"- F(4) = {FibonacciViaRecursion(4)}");
            Console.WriteLine($"- F(5) = {FibonacciViaRecursion(5)}");
            Console.WriteLine($"- F(6) = {FibonacciViaRecursion(6)}");
            Console.WriteLine($"- F(7) = {FibonacciViaRecursion(7)}");
            Console.WriteLine($"- F(8) = {FibonacciViaRecursion(8)}");

            Console.ReadKey();
        }

        static uint FibonacciViaRecursion(uint count, uint i = 2, uint n1 = 0, uint n2 = 1)
        {
            if (count == 0)
            {
                return 0;
            }
            if (count == 1)
            {
                return 1;
            }
            if (count > i)
            {
                uint temp = n2;
                n2 = n1 + n2;
                n1 = temp;
                i++;
                return FibonacciViaRecursion(count, i, n1, n2);
            }

            return n1 + n2;
        }
        static uint FibonacciViaCycle(uint count)
        {
            uint i = 2;
            uint n1 = 0;
            uint n2 = 1;
            uint temp;

            if (count == 0)
            {
                return 0;
            }
            if (count == 1)
            {
                return 1;
            }
            while (i < count)
            {
                temp = n2;
                n2 = n1 + n2;
                n1 = temp;
                i++;
            }

            return n1 + n2;
        }
    }
}
