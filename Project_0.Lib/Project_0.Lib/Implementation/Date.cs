using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class Date
    {
        public DateTime dateTime { get; set; }

        public int Day { get; set; }
        public int Month { get; set; }

        public int Hour { get; set; }

        public static bool operator == (Date dt1, Date dt2)
        {
            if ((dt1.Month == dt2.Month) && (dt2.Day == dt2.Day))
                return true;
            else
                return false;
        }

        public static bool operator != (Date dt1, Date dt2)
        {
            if ((dt1.Month == dt2.Month) || (dt2.Day == dt2.Day))
                return false;
            else
                return true;
        }

        //overload the "==" operator so first month is compared, then day
    }
}
