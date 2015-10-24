using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorter.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SorterAttribute : System.Attribute
    {
        public string SorterName { get; set; }
    }
}
