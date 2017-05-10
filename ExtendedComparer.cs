using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class ExtendedComparer : IComparer<Car>
    {
        bool dec = false;
        public ExtendedComparer(bool Dec)
        {
            dec = Dec;
        }
        public ExtendedComparer() { }
        public int Compare(Car x, Car y)
        {
            if (!dec)
            {
                if (x.Engine.HorsePower == y.Engine.HorsePower)
                    return 0;
                if (x.Engine.HorsePower > y.Engine.HorsePower)
                    return 1;

                return -1;
            }
            else
            {
                if (x.Engine.HorsePower == y.Engine.HorsePower)
                    return 0;
                if (x.Engine.HorsePower > y.Engine.HorsePower)
                    return -1;

                return 1;
            }
        }
    }
}
