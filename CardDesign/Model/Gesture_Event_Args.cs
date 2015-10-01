using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardDesign
{
    public class Gesture_Event_Args:EventArgs
    {
        private My_Point[] gesturePoints;

        public My_Point[] GesturePoints
        {
            get { return gesturePoints; }
            set { gesturePoints = value; }
        }
        private object[] senders;

        public object[] Senders
        {
            get { return senders; }
            set { senders = value; }
        }
    }
}
