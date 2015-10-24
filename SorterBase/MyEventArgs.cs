using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorter.Base
{
    public class ArrayElement
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public string Value { get; protected set; }
        public ArrayElement(int x, int y, string value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }

    public class SwapElementsArgs : EventArgs
    {
        public ArrayElement FirstSwapElement { get; set; }
        public ArrayElement SecondSwapElement { get; set; }

        public SwapElementsArgs(ArrayElement firstSwapElement = null, ArrayElement secondSwapElement = null)
        {
            FirstSwapElement = firstSwapElement;
            SecondSwapElement = secondSwapElement;
        }
    }

    

    public class ExceptionArgs : EventArgs
    {
        public Exception Exception { get; set; }

        public ExceptionArgs(Exception ex)
        {
            Exception = ex;
        }
    }
}
