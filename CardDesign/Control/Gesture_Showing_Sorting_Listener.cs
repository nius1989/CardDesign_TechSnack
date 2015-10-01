using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    /// <summary>
    /// Listener for the ShowingGroup Gesture (Long Press the Sorting Button)
    /// Defines actions for gesture register, continue, terminate and fail states.
    /// </summary>
    class Gesture_Showing_Sorting_Listener:Gesture_Listener
    {

        /// <param name="gestureControler">Should be the one created in the MainWindow</param>
        /// <param name="gestureEvent">The gesture variable which fire the event</param>
        public Gesture_Showing_Sorting_Listener(Gesture_Controler gestureControler, Gesture_Event_Showing_Sorting gestureEvent)
            : base(gestureControler, gestureEvent)
        {
            
        }

        public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Menu_Sort_Button b = gEventArgs.Senders[0] as Menu_Sort_Button;
            gestureControler.MainWindow.SortingGestureLayer.RemoveLines(b.GroupID);
            base.TerminateGesture(sender,gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            base.ContinueGesture(sender,gEventArgs);
        }

        public override void RegisterGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Menu_Sort_Button b=gEventArgs.Senders[0] as Menu_Sort_Button;
            gestureControler.MainWindow.SortingGestureLayer.ShowLines(b.GroupID);
            base.RegisterGesture(sender, gEventArgs);
        }
        public override void FailGesture(object sender, Gesture_Event_Args gEventArgs) 
        {
            Menu_Sort_Button b = gEventArgs.Senders[0] as Menu_Sort_Button;
            gestureControler.MainWindow.SortingGestureLayer.RemoveLines(b.GroupID);
            base.FailGesture(sender, gEventArgs);
        }
    }
}
