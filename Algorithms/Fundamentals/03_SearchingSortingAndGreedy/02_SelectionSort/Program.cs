﻿using System;
using System.Linq;

namespace _02_SelectionSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            SelectionSort(numbers);
            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void SelectionSort(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                var minIndex = i;
                var minNumber = numbers[i];

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < minNumber)
                    {
                        minNumber = numbers[j];
                        minIndex = j;
                    }
                }

                Swap(numbers,i,minIndex);
            }
        }

        private static void Swap(int[] numbers, in int first, in int second)
        {
            var temp = numbers[first];
            numbers[first] = numbers[second];
            numbers[second] = temp;
        }
    }
}
