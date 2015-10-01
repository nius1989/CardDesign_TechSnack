using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CardDesign
{
    class Gesture_Copying_Listener:Gesture_Listener
    {
        private Point lastPosi = new Point(0, 0);
        public Gesture_Copying_Listener(Gesture_Controler gestureControler, Gesture_Event_Copying gestureEvent)
            : base(gestureControler, gestureEvent)
        {
            
        }
        public override void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            base.TerminateGesture(sender, gEventArgs);
        }

        public override void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            if(gEventArgs.Senders.Length>1){
                Card card2 = gEventArgs.Senders[0] as Card;
                card2.MoveCard(gEventArgs.GesturePoints[0].CurrentPoint.Position.X - lastPosi.X,
                    gEventArgs.GesturePoints[0].CurrentPoint.Position.Y - lastPosi.Y,
                    0);
                lastPosi = gEventArgs.GesturePoints[0].CurrentPoint.Position;
            }
            base.ContinueGesture(sender, gEventArgs);
        }

        public override void RegisterGesture(object sender, Gesture_Event_Args gEventArgs)
        {

            Card card=gEventArgs.Senders[0] as Card;
            Card card2=gestureControler.MainWindow.CardControler.CopyCard(card);
            if (card2 != null) {
                gEventArgs.Senders[1] = card2;
            }
            lastPosi = gEventArgs.GesturePoints[0].CurrentPoint.Position;
            base.RegisterGesture(sender, gEventArgs);
        }

        public override void FailGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            base.FailGesture(sender, gEventArgs);
        }
    }
}
