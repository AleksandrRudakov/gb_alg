using System;

namespace less_1_2
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        public static int StrangeSum(int[] inputArray)
        {
            int sum = 0; // O(1)
            for (int i = 0; i < inputArray.Length; i++) // O(n)
            {
                for (int j = 0; j < inputArray.Length; j++) // O(n)
                {
                    for (int k = 0; k < inputArray.Length; k++) // O(n)
                    {
                        int y = 0; // O(1)
                        if (j != 0) // O(1)
                        {
                            y = k / j; // O(1)
                        }
                        sum += inputArray[i] + i + k + j + y; // O(1)
                    }
                }
            }
            return sum;

            // Итоговая сложность функции O(n^3), так как можем отбросить O(1), то остается 3 цикла вложенных друг в друга, сложность каждого из которых O(n),
            // в итоге получаем сложность функции O(n^3)
        }
    }
}
