using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    /// <summary>
    /// Listener for the Linking Gesture (Drag and link)
    /// Defines actions for gesture register, continue, terminate and fail states.
    /// </summary>
    class Gesture_Linking_Listener:Gesture_Listener
    {
        public Gesture_Linking_Listener(Gesture_Controler gestureControler, Gesture_Event_Linking gestureEvent)
            : base(gestureControler, gestureEvent)
        {
            
        }
        public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Gesture_Event_Linking gesture = sender as Gesture_Event_Linking;
            if (gEventArgs.Senders != null && gEventArgs.Senders[1] != null)
            {
                gestureControler.MainWindow.LinkingGestureLayer.Move(gesture);
            }
            else {
                gestureControler.MainWindow.LinkingGestureLayer.Remove(gesture);
            }

            base.TerminateGesture(sender, gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Gesture_Event_Linking gesture = sender as Gesture_Event_Linking;
            gestureControler.MainWindow.LinkingGestureLayer.Move(gesture);
            base.ContinueGesture(sender, gEventArgs);
        }

        public override void RegisterGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Gesture_Event_Linking gesture = sender as Gesture_Event_Linking;
            gestureControler.MainWindow.LinkingGestureLayer.Add(gesture);
            base.RegisterGesture(sender, gEventArgs);
        }

        public override void FailGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            base.FailGesture(sender, gEventArgs);
        }
    }
}
