using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    /// <summary>
    /// Listener for the Cutting Gesture (Swapping on the line)
    /// Defines actions for gesture register, continue, terminate and fail states.
    /// </summary>
    class Gesture_Cutting_Sorting_Listener:Gesture_Listener
    {        
        /// <param name="gestureControler">Should be the one created in the MainWindow</param>
        /// <param name="gestureEvent">The gesture variable which fire the event</param>
        public Gesture_Cutting_Sorting_Listener(Gesture_Controler gestureControler, Gesture_Event_Cutting_Sorting gestureEvent)
            : base(gestureControler, gestureEvent)
        {
            
        }
         public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Card c = gEventArgs.Senders[1] as Card;
            Menu_Sort_Button b = gEventArgs.Senders[0] as Menu_Sort_Button;
            Group_List.RemoveCard(b.GroupID, c);
            gestureControler.MainWindow.SortingGestureLayer.Repaint();

            
            base.TerminateGesture(sender, gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            base.ContinueGesture(sender, gEventArgs);
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
