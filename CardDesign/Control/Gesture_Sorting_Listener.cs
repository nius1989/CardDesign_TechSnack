using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CardDesign
{
    /// <summary>
    /// Listener for the Sorting Gesture (Drag and drop to the icon)
    /// Defines actions for gesture register, continue, terminate and fail states.
    /// </summary>
    class Gesture_Sorting_Listener : Gesture_Listener
    {

        /// <param name="gestureControler">Should be the one created in the MainWindow</param>
        /// <param name="gestureEvent">The gesture variable which fire the event</param>
        public Gesture_Sorting_Listener(Gesture_Controler gestureControler, Gesture_Event_Sorting gestureEvent)
            : base(gestureControler, gestureEvent)
        {
            
        }

        public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Card c = gEventArgs.Senders[0] as Card;
            Menu_Sort_Button b = gEventArgs.Senders[1] as Menu_Sort_Button;
            if (!gestureControler.MainWindow.MenuLayer.IsButtonInOriginPos(b) )
            {
                c.SortToGroup(b.GroupID);
                Group_List.Add(b.GroupID, c);
                My_Point point = gEventArgs.GesturePoints[0];
                Vector v = new Vector(c.PreviousPostion.X - c.CurrentPosition.X, c.PreviousPostion.Y - c.CurrentPosition.Y);
                v.Normalize();
                c.MoveCard(v.X * 150, v.Y * 150, 0.5);                
            }
            if (Group_List.CardGroups.ContainsKey(b.GroupID))
            {
                Card[] cards = Group_List.CardGroups[b.GroupID].ToArray();
                foreach (Card cc in cards) { cc.Dehightlight(); }
            }
            base.TerminateGesture(sender,gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {

            base.ContinueGesture(sender,gEventArgs);
        }

        public override void RegisterGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Menu_Sort_Button b = gEventArgs.Senders[1] as Menu_Sort_Button;
            if (Group_List.CardGroups.ContainsKey(b.GroupID) && !gestureControler.MainWindow.MenuLayer.IsButtonInOriginPos(b))
            {
                Card[] cards = Group_List.CardGroups[b.GroupID].ToArray();
                foreach (Card c in cards) { c.Hightlight(Colors.Gold); }
            }
            base.RegisterGesture(sender, gEventArgs);
        }
        public override void FailGesture(object sender, Gesture_Event_Args gEventArgs) 
        {
            Menu_Sort_Button b = gEventArgs.Senders[1] as Menu_Sort_Button;

            if (Group_List.CardGroups.ContainsKey(b.GroupID))
            {
                Card[] cards = Group_List.CardGroups[b.GroupID].ToArray();
                foreach (Card c in cards) { c.Dehightlight(); }
            }
            base.FailGesture(sender, gEventArgs);
        }
    }
}
