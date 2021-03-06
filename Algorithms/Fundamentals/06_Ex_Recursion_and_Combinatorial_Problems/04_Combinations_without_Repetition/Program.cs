﻿using System;

namespace _04_Combinations_without_Repetition
{
    class Program
    {
        private static string[] elements;
        private static int k;

        private static string[] combinations;

        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            elements = new string[num];
            for (int i = 1; i <= num; i++)
            {
                elements[i - 1] = i.ToString();
            }
            k = int.Parse(Console.ReadLine());

            combinations = new string[k];


            Combinations(0, 0);
        }

        private static void Combinations(int index, int startIndex)
        {
            if (index >= combinations.Length)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }

            for (int i = startIndex; i < elements.Length; i++)
            {
                combinations[index] = elements[i];
                Combinations(index + 1, i + 1);
            }
        }
    }
}
