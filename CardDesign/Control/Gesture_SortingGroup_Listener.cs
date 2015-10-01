using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    /// <summary>
    /// Listener for the SortingGroup Gesture (Four finger down and drag and drop onto the icon)
    /// Defines actions for gesture register, continue, terminate and fail states.
    /// </summary>
    class Gesture_SortingGroup_Listener:Gesture_Listener
    {

        /// <param name="gestureControler">Should be the one created in the MainWindow</param>
        /// <param name="gestureEvent">The gesture variable which fire the event</param>
        public Gesture_SortingGroup_Listener(Gesture_Controler gestureControler, Gesture_Event_SortingGroup nameclusterGesture)
            : base(gestureControler, nameclusterGesture)
        {
            
        }

        public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Menu_Sort_Button button = gEventArgs.Senders[0] as Menu_Sort_Button;
            Card[] cards = new Card[gEventArgs.Senders.Length - 1];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = gEventArgs.Senders[i + 1] as Card;
            }
            foreach (Card card in cards)
            {
                if (!gestureControler.GroupingControler.ContainCard(button.GroupID, card))
                {
                    gestureControler.GroupingControler.GroupCard(button.GroupID, card);
                }
                else
                {
                    gestureControler.GroupingControler.RemoveCard(button.GroupID, card);
                }
            }
            base.TerminateGesture(sender, gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {

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
