using System;

namespace less_2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] mass = { 3, 5, 7, 10, 12, 51, 53, 79, 107};
            Console.WriteLine($"int[] mass = 3, 5, 7, 10, 12, 51, 53, 79, 107; searchValue = 51; mid = {BinarySearch(mass, 51)}");
            Console.WriteLine($"int[] mass = 3, 5, 7, 10, 12, 51, 53, 79, 107; searchValue = 11; mid = {BinarySearch(mass, 11)}");
            Console.WriteLine($"int[] mass = 3, 5, 7, 10, 12, 51, 53, 79, 107; searchValue = 10; mid = {BinarySearch(mass, 10)}");
            Console.WriteLine($"int[] mass = 3, 5, 7, 10, 12, 51, 53, 79, 107; searchValue = 79; mid = {BinarySearch(mass, 79)}");
            Console.WriteLine($"int[] mass = 3, 5, 7, 10, 12, 51, 53, 79, 107; searchValue = 3; mid = {BinarySearch(mass, 3)}");

            Console.ReadKey();
        }
        public static int BinarySearch(int[] inputArray, int searchValue)
        {
            int min = 0;
            int max = inputArray.Length - 1; // O(1)
            while (min <= max) // O(n/2)
            {
                int mid = (min + max) / 2; // O(1)
                if (searchValue == inputArray[mid]) // O(1)
                {
                    return mid;
                }
                else if (searchValue < inputArray[mid]) // O(1)
                {
                    max = mid - 1; // O(1)
                }
                else
                {
                    min = mid + 1; // O(1)
                }
            }
            return -1;
            // Сложность алгоритма O(n/2)
        }
    }
}
