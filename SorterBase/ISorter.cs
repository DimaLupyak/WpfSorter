using System;

namespace Sorter.Base
{
    public interface ISorter <T> where T : IComparable<T>
    {
        event EventHandler SwapElements;
        /// <summary>
        /// Return sorted array.
        /// </summary>
        /// <param name="array">Array to sort</param>
        /// <returns>Sorted array</returns>
        T[] Sort(T[] array);
        /// <summary>
        /// Return the sorted jagged array.
        /// </summary>
        /// <param name="array">Jagged array to sort</param>
        /// <returns>Sorted jagged array</returns>
        T[][] Sort(T[][] array);
    }
}
