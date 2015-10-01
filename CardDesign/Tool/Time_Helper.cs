using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    class Time_Helper
    {
        static double MININTERVAL = 20;
        public static double getTimeIntervel(DateTime time1, DateTime time2)
        {
            long intv = time1.Ticks - time2.Ticks;
            TimeSpan ts = new TimeSpan(intv);
            return ts.TotalMilliseconds;
        }

        public static bool isSameTime(DateTime time1, DateTime time2)
        {
            long intv = time1.Ticks - time2.Ticks;
            TimeSpan ts = new TimeSpan(intv);
            return Math.Abs(ts.TotalMilliseconds) < MININTERVAL;
        }
    }
}
