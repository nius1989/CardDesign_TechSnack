using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    /// <summary>
    /// Listener for the Grouping Gesture (Four finger down)
    /// Defines actions for gesture register, continue, terminate and fail states.
    /// </summary>
    class Gesture_Grouping_Listener:Gesture_Listener
    {

        /// <param name="gestureControler">Should be the one created in the MainWindow</param>
        /// <param name="gestureEvent">The gesture variable which fire the event</param>
        public Gesture_Grouping_Listener(Gesture_Controler gestureControler, Gesture_Event_Grouping gestureEvent)
            : base(gestureControler, gestureEvent)
        {
            
        }
        public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            foreach (Card c in gEventArgs.Senders)
            {
                gestureControler.MainWindow.LinkingGestureLayer.Move(c);
                c.Dehightlight();
            }
            gestureControler.MainWindow.GroupingGestureLayer.Remove(sender as Gesture_Event_Grouping);
            base.TerminateGesture(sender, gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Gesture_Event_Grouping gesture = sender as Gesture_Event_Grouping;            
            Card[] cards = gEventArgs.Senders as Card[];
            if (!gesture.IsGrouping())
            {
                foreach (Card c in cards)
                {
                    c.MoveCard(gesture.Vector.X, gesture.Vector.Y, 0);
                }
                gestureControler.MainWindow.GroupingGestureLayer.Remove(gesture);
            }
            base.ContinueGesture(sender, gEventArgs);
        }

        public override void RegisterGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            foreach (Card c in gEventArgs.Senders)
            {
                c.Hightlight();
            }
            gestureControler.MainWindow.GroupingGestureLayer.Add(sender as Gesture_Event_Grouping);
            base.RegisterGesture(sender, gEventArgs);
        }

        public override void FailGesture(object sender, Gesture_Event_Args gEventArgs)
        {            
            foreach (Card c in gEventArgs.Senders)
            {
                c.Dehightlight();
            }
            gestureControler.MainWindow.GroupingGestureLayer.Remove(sender as Gesture_Event_Grouping);
            base.FailGesture(sender, gEventArgs);
        }
    }
}
