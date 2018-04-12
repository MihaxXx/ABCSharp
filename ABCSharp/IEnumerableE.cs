using System;
using System.Collections.Generic;

namespace ABCSharp
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableE
    {
        /// <summary>
        /// Prints elements of sequence, separated by deliminator
        /// </summary>
        public static void Print<T>(this IEnumerable<T> sequence, string deliminator = " ")
        {
            foreach (var element in sequence)
                Console.Write($"{element}{deliminator}");
        }

        /// <summary>
        /// Prints elements of sequence, separated by deliminator and adds newline in the end
        /// </summary>
        public static void PrintLn<T>(this IEnumerable<T> sequence, string deliminator = " ")
        {
            sequence.Print(deliminator);
            Console.WriteLine();
        }
    }
}
