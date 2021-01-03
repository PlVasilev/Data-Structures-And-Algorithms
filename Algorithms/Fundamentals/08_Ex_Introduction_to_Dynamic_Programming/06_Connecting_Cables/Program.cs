﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace _06_Connecting_Cables
{
    class Program
    {
        static void Main(string[] args)
        {
            var input1 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var input2 = new int[input1.Length];
            for (int i = 0; i < input2.Length; i++)
                input2[i] = i + 1;
            

            var table = new int[input1.Length + 1, input2.Length + 1];
            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (input1[r - 1] == input2[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max(table[r - 1, c], table[r, c - 1]);
                    }
                }
            }
            Console.WriteLine($"Maximum pairs connected: {table[input1.Length, input2.Length]}");
            var row = input1.Length;
            var col = input2.Length;
            var pairs = new Stack<int>();

            while (row > 0 && col > 0)
            {
                if (input1[row - 1] == input2[col - 1])
                {
                    
                    row -= 1;
                    col -= 1;
                    pairs.Push(input1[row]);
                }
                else if (table[row - 1, col] > table[row, col - 1])
                {
                    row -= 1;
                }
                else
                {
                    col -= 1;
                }
            }
            Console.WriteLine(string.Join(" ", pairs));
        }
    }
}
