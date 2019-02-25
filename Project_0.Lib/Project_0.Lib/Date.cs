using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }

        //overload the "==" operator so first month is compared, then day
    }
}
