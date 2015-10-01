using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    /// <summary>
    /// Listener for the Linking Gesture (Three finger long press)
    /// Defines actions for gesture register, continue, terminate and fail states.
    /// </summary>
    class Gesture_Showing_Listener:Gesture_Listener
    {

        /// <param name="gestureControler">Should be the one created in the MainWindow</param>
        /// <param name="gestureEvent">The gesture variable which fire the event</param>
        public Gesture_Showing_Listener(Gesture_Controler gestureControler, Gesture_Event_Showing gestureEvent)
            : base(gestureControler, gestureEvent)
        {
            
        }
        public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            
            Card card = gEventArgs.Senders[0] as Card;
            foreach (Card c in Card_List.CardList)
            {
                if (c != card)
                {
                    if (c.UID == card.UID)
                    {
                        c.Dehightlight();
                    }
                }
            }
            base.TerminateGesture(sender, gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            base.ContinueGesture(sender, gEventArgs);
        }

        public override void RegisterGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Card card = gEventArgs.Senders[0] as Card;
            foreach (Card c in Card_List.CardList) {
                if (c != card) {
                    if (c.UID == card.UID) {
                        c.Hightlight();
                    }
                }
            }
            base.RegisterGesture(sender, gEventArgs);
        }
        public override void FailGesture(object sender, Gesture_Event_Args gEventArgs)
        {      
            gestureControler.MainWindow.SortingGestureLayer.RemoveAll();
            base.FailGesture(sender, gEventArgs);
        }
    }
}
