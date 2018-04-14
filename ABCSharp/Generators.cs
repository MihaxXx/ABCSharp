using System.Collections.Generic;

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
    }
}