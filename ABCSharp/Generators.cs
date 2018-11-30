using System;
using System.Collections.Generic;
using System.Linq;

namespace ABCSharp
{
    public static class Generators
    {
        /// <summary>
        /// Returns sequence of integers from begin to end (excluded)
        /// </summary>
        public static IEnumerable<int> Range(int begin, int end)
        {
            var delta = begin < end ? 1 : -1;
            for (var i = begin; i != end; i += delta)
                yield return i;
        }

        /// <summary>
        /// Returns sequence of integers from zero to end (excluded)
        /// </summary>
        public static IEnumerable<int> Range(int end)
        {
            for (var i = 0; i != end; ++i)
                yield return i;
        }

		/// <summary>
		/// Returns sequence of double from begin to end (excluded) and step h
		/// </summary>
	    public static IEnumerable<double> Range(double begin, double end, double h = 1)
	    {
			if (begin < end && h < 0 || begin > end && h > 0)
				throw new ArgumentException("Incorrect range");
		    for (var i = begin; i < end; i += h)
			    yield return i;
	    }
        /// <summary>
        /// Returns sequence of double from begin to end (excluded) and step h
        /// </summary>
        public static IEnumerable<double> Range(int begin, int end, int h)
        {
            if (h == 0)
                throw new System.ArgumentException("h=0", nameof(h));
            if (!((h > 0) && (end < begin) || (h < 0) && (end > begin)))
            {
                if (end > begin)
                {
                    for (var i = begin; i < end; i += h)
                        yield return i;
                }
                else
                {
                    for (var i = begin; i > end; i += h)
                        yield return i;
                }
            }
        }

        /// <summary>
        /// Returns array filled with <paramref name="a"/>
        /// </summary>
        /// <returns>The array.</returns>
        /// <param name="a">Type <typeparamref name="T"/> objects.</param>
        /// <typeparam name="T">The type parameter.</typeparam>
        public static T[] Arr<T>(params T[] a)
        {
            return a;
        }

        /// <summary>
        /// Returns array filled with values from sequence
        /// </summary>
        /// <returns>The array</returns>
        /// <param name="a">The sequence</param>
        /// <typeparam name="T">The type parameter</typeparam>
        public static T[] Arr<T>(IEnumerable<T> a)
        {
            return a.ToArray();
        }

        /// <summary>
        /// Returnt array size of <paramref name="n"/> filled with random int values 
        /// </summary>
        /// <returns>The random int.</returns>
        /// <param name="n">Number of elements.</param>
        /// <param name="a">The lower bound of posymbsible values.</param>
        /// <param name="b">The upper bound of posymbsible values.</param>
        public static int[] ArrRandomInt(int n = 10, int a = 0, int b = 100)
        {
            Random r = new Random();
            var res = new int[n];
            for (int i = 0; i < n; i++)
                res[i] = r.Next(a, b);
            return res;
        }

        /// <summary>
        /// Returnt array size of <paramref name="n"/> filled with random int values 
        /// </summary>
        /// <returns>The random int array.</returns>
        /// <param name="n">Number of elements.</param>
        /// <param name="a">The lower bound of posymbsible values.</param>
        /// <param name="b">The upper bound of posymbsible values.</param>
        public static int[] ArrRandom(int n = 10, int a = 0, int b = 100) => ArrRandomInt(n, a, b);

        /// <summary>
        /// Returnt array size of <paramref name="n"/> filled with random double values 
        /// </summary>
        /// <returns>The random double array.</returns>
        /// <param name="n">Number of elements.</param>
        /// <param name="a">The lower bound of posymbsible values.</param>
        /// <param name="b">The upper bound of posymbsible values.</param>
        public static double[] ArrRandomDouble(int n = 10, double a = 0, double b = 10)
        {
            if (a > b)
                (a, b) = (b, a);
            Random r = new Random();
            var res = new double[n];
            for (var i = 0; i < n; i++)
                res[i] = (r.NextDouble() * Math.Abs(b - a)) + a;
            return res;
        }

        /// <summary>
        /// Returns array size of <paramref name="count"/> filled with <paramref name="f"/>(i) values
        /// </summary>
        /// <returns>The array of <typeparamref name="T"/> elements.</returns>
        /// <param name="count">Number of elements.</param>
        /// <param name="f">The function.</param>
        /// <typeparam name="T">The type parameter.</typeparam>
        public static T[] ArrGen<T>(int count, Func<int, T> f)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count), "Parameter count must be > 0");
            var res = new T[count];
            for (var i = 0; i < count; i++)
                res[i] = f(i);
            return res;
        }

        /// <summary>
        /// Returns array size of <paramref name="count"/> filled with <paramref name="f"/>(i) values starting from i=<paramref name="from"/>
        /// </summary>
        /// <returns>The array of <typeparamref name="T"/> elements.</returns>
        /// <param name="count">Number of elements.</param>
        /// <param name="f">The function.</param>
        /// <param name="from">The initial i</param>
        /// <typeparam name="T">The type parameter.</typeparam>
        public static T[] ArrGen<T>(int count, Func<int, T> f, int from)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count), "Parameter count must be > 0");
            var res = new T[count];
            for (var i = 0; i < count; i++)
                res[i] = f(from + i);
            return res;
        }

        /// <summary>
        /// Returns array size of <paramref name="count"/> starting from <paramref name="first"/> using <paramref name="next"/> function to pasymbs from previous to next
        /// </summary>
        /// <returns>The array of <typeparamref name="T"/> elements.</returns>
        /// <param name="count">Number of elements.</param>
        /// <param name="first">First element.</param>
        /// <param name="next">The function.</param>
        /// <typeparam name="T">The type parameter.</typeparam>
        public static T[] ArrGen<T>(int count, T first, Func<T, T> next)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count), "Parameter count must be > 0");
            var res = new T[count];
            res[0] = first;
            for (var i = 1; i < count; i++)
                res[i] = next(res[i - 1]);
            return res;
        }

        /// <summary>
        /// Returns array size of <paramref name="count"/> starting from <paramref name="first"/> and <paramref name="second"/> using <paramref name="next"/> function to pasymbs from two previous to next
        /// </summary>
        /// <returns>The array of <typeparamref name="T"/> elements.</returns>
        /// <param name="count">Number of elements.</param>
        /// <param name="first">First element.</param>
        /// <param name="second">Second element.</param>
        /// <param name="next">The function.</param>
        /// <typeparam name="T">The type parameter.</typeparam>
        public static T[] ArrGen<T>(int count, T first, T second, Func<T, T, T> next)
        {
            if (count < 2)
                throw new ArgumentOutOfRangeException(nameof(count), "Parameter count must be > 1");
            var res = new T[count];
            res[0] = first;
            res[0] = second;
            for (var i = 2; i < count; i++)
                res[i] = next(res[i - 2], res[i - 1]);
            return res;
        }

        /// <summary>
        /// Returns array size of <paramref name="count"/> filled with <paramref name="x"/>
        /// </summary>
        /// <returns>The array.</returns>
        /// <param name="count">Number of elements.</param>
        /// <param name="x">The element.</param>
        /// <typeparam name="T">The type parameter.</typeparam>
        public static T[] ArrFill<T>(int count, T x)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count), "Parameter count must be > 0");
            var res = new T[count];
            for (var i = 0; i < count; i++)
                res[i] = x;
            return res;
        }

        /// <summary>
        /// Returns the array of <paramref name="n"/> integers read from console
        /// </summary>
        /// <returns>The array of integers.</returns>
        /// <param name="n">Number of elements.</param>
        public static int[] ReadArrInt(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "Parameter n must be > 0");
            var res = new int[n];
            var i = 0;
            while (i < n)
            {
                var symbs = Console.ReadLine().Split(' ').Where(symb => symb != string.Empty);
                foreach (var s in symbs)
                    res[i++] = int.Parse(s);
            }
            return res;
        }

        /// <summary>
        /// Returns the array of <paramref name="n"/> double elements read from console
        /// </summary>
        /// <returns>The array of double elements.</returns>
        /// <param name="n">Number of elements.</param>
        public static double[] ReadArrDouble(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "Parameter n must be > 0");
            var res = new double[n];
            var i = 0;
            while (i < n)
            {
                var symbs = Console.ReadLine().Split(' ').Where(symb => symb != string.Empty);
                foreach (var s in symbs)
                    res[i++] = double.Parse(s);
            }
            return res;
        }

        /// <summary>
        /// Returns the array of <paramref name="n"/> strings read from console
        /// </summary>
        /// <returns>The array of strings.</returns>
        /// <param name="n">Number of elements.</param>
        public static string[] ReadArrString(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "Parameter n must be > 0");
            var res = new string[n];
            for (var i = 0; i < n; i++)
                res[i] = Console.ReadLine();
            return res;
        }

        /// <summary>
        /// Prompts for input and returns the array of <paramref name="n"/> integers read from console
        /// </summary>
        /// <returns>The array of integers.</returns>
        /// <param name="prompt">The prompt.</param>
        /// <param name="n">Number of elements.</param>
        public static int[] ReadArrInt(string prompt, int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "Parameter n must be > 0");
            Console.Write(prompt);
            var res = new int[n];
            var i = 0;
            while (i < n)
            {
                var symbs = Console.ReadLine().Split(' ').Where(symb => symb != string.Empty);
                foreach (var s in symbs)
                    res[i++] = int.Parse(s);
            }
            return res;
        }

        /// <summary>
        /// Prompts for input and returns the array of <paramref name="n"/> double elements read from console
        /// </summary>
        /// <returns>The array of double elements.</returns>
        /// <param name="prompt">The prompt.</param>
        /// <param name="n">Number of elements.</param>
        public static double[] ReadArrDouble(string prompt, int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "Parameter n must be > 0");
            Console.Write(prompt);
            var res = new double[n];
            var i = 0;
            while (i < n)
            {
                var symbs = Console.ReadLine().Split(' ').Where(symb => symb != string.Empty);
                foreach (var s in symbs)
                    res[i++] = double.Parse(s);
            }
            return res;
        }

        /// <summary>
        /// Prompts for input and returns the array of <paramref name="n"/> strings read from console
        /// </summary>
        /// <returns>The array of strings.</returns>
        /// <param name="prompt">The prompt.</param>
        /// <param name="n">Number of elements.</param>
        public static string[] ReadArrString(string prompt, int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "Parameter n must be > 0");
            Console.Write(prompt);
            var res = new string[n];
            for (var i = 0; i < n; i++)
                res[i] = Console.ReadLine();
            return res;
        }
    }
}