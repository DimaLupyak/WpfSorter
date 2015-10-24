using System;

namespace Sorter.Base
{
    abstract public class ASorter<T> : ISorter<T> where T : IComparable<T> 
    {
        protected int currentArrayLine = 0;

        protected void Swap(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        abstract public T[] Sort(T[] array);
        
        public T[][] Sort(T[][] array)
        {
            if (array == null) return array;
            for (int i = 0; i < array.Length; i++)
            {
                currentArrayLine = i;
                array[i] = Sort(array[i]);                
            }
            return array;
        }


        public event EventHandler SwapElements;

        protected void PublishSwapElements(SwapElementsArgs args)
        {
            if (SwapElements != null)
            {
                SwapElements(this, args);
            }
        }
        
    }     
}
