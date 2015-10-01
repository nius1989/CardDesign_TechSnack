using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CardDesign
{
    /// <summary>
    /// Listener for the Emphasizing Gesture (Long press the card)
    /// Defines actions for gesture register, continue, terminate and fail states.
    /// </summary>
    class Gesture_Emphasizing_Listener:Gesture_Listener
    {

        /// <param name="gestureControler">Should be the one created in the MainWindow</param>
        /// <param name="gestureEvent">The gesture variable which fire the event</param>
        public Gesture_Emphasizing_Listener(Gesture_Controler gestureControler, Gesture_Event_Emphasizing gestureEvent)
            : base(gestureControler, gestureEvent)
        {
            
        }

        public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Card c = gEventArgs.Senders[0] as Card;
            double newsize = (sender as Gesture_Event_Emphasizing).GetSize();
            double oldsize = (sender as Gesture_Event_Emphasizing).StartSize;
            if (newsize < oldsize) {
                c.ResetBrightness();
            }
            base.TerminateGesture(sender,gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Card c = gEventArgs.Senders[0] as Card;
            double newsize = (sender as Gesture_Event_Emphasizing).GetSize();
            double oldsize = (sender as Gesture_Event_Emphasizing).StartSize;
            if (newsize != oldsize)
            {
                c.ChangeBrightness(newsize, oldsize);
            }
            else {                
                return;
            }
            base.ContinueGesture(sender,gEventArgs);
        }

        public override void RegisterGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            base.RegisterGesture(sender, gEventArgs);
        }
        public override void FailGesture(object sender, Gesture_Event_Args gEventArgs)
        {          
            base.FailGesture(sender, gEventArgs);
        }
    }
}
