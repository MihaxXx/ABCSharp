using System;

namespace ABCSharp
{
    public static class Utility
    {
        /// <summary>
        /// Self-explainatory
        /// </summary>
        public static void Swap<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Attempts to read int number from console. Unsafe.
        /// </summary>
        public static int ReadInt() =>
            int.Parse(Console.ReadLine());

        /// <summary>
        /// Attempts to read integer from console. Returns true if succeeded, false otherwise.
        /// </summary>
        public static bool TryReadInt(out int result)
        {
            try
            {
                result = ReadInt();
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        /// <summary>
        /// Attempts to read double from console. Unsafe.
        /// </summary>
        public static double ReadDouble() =>
            double.Parse(Console.ReadLine());

        /// <summary>
        /// Attempts to read double from console. Returns true if succeeded, false otherwise.
        /// </summary>
        public static bool TryReadDouble(out double result)
        {
            try
            {
                result = ReadDouble();
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }
    }
}