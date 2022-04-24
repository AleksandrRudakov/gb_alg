using System;

namespace less_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число:");

            try
            {
                int n = Convert.ToInt32(Console.ReadLine());

                int d = 0, i = 2;

                while (i < n)
                {
                    if (n % i == 0)
                    {
                        d++;
                    }
                    i++;
                }

                if (d == 0)
                {
                    Console.WriteLine("Простое");
                }
                else
                {
                    Console.WriteLine("Не простое");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка ввода!");
            }

            Console.ReadKey();
        }
    }
}
