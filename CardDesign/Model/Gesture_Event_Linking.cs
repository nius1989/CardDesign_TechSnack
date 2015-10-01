using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{

    public class Gesture_Event_Linking : Gesture_Event
    {
        public enum DIRECTION { MALE, FEMALE, DOUBLE }
        Card card1;

        public Card Card1
        {
            get { return card1; }
            set { card1 = value; }
        }
        Card card2;

        public Card Card2
        {
            get { return card2; }
            set { card2 = value; }
        }
        DIRECTION direction = DIRECTION.MALE;

        internal DIRECTION Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Gesture_Event_Linking)
            {
                Gesture_Event_Linking link = obj as Gesture_Event_Linking;
                return link.Card1 == this.card1 && link.Card2 == this.card2;
            }
            else {
                return false;
            }
        }
        public override void Register(object[] senders, My_Point[] myPoints)
        {
            if (myPoints != null)
            {
                Points = myPoints;
                Senders = senders;
                card1 = (Card)senders[0];
                Gesture_Event_Args gestureEventArgs = new Gesture_Event_Args();
                gestureEventArgs.GesturePoints = myPoints;
                gestureEventArgs.Senders = senders;
                this.Status = GESTURESTATUS.REGISTER;
                OnRegistered(this, gestureEventArgs);
            }
        }
        public override void Continue(object[] senders, My_Point[] myPoints)
        {
            bool isValid = checkValid(senders, myPoints);
            if (myPoints != null && senders != null && isValid)
            {
                Gesture_Event_Args gestureEventArgs = new Gesture_Event_Args();
                gestureEventArgs.GesturePoints = myPoints;
                gestureEventArgs.Senders = senders;
                this.Status = GESTURESTATUS.CONTINUE;
                OnContinued(this, gestureEventArgs);
            }
            if (!isValid)
            {
                Fail();
                return;
            }
            if (checkTerminate(senders, myPoints))
            {
                Terminate(senders, myPoints);
            }
        }
        protected override bool checkTerminate(object[] senders, My_Point[] myPoints)
        {
            return !myPoints[0].IsLive;
        }
        protected override bool checkValid(object[] senders, My_Point[] myPoints)
        {
            return true;
        }
        public override void Terminate(object[] senders, My_Point[] myPoints)
        {
            if (myPoints != null && senders != null)
            {
                Gesture_Event_Args gestureEventArgs = new Gesture_Event_Args();
                gestureEventArgs.GesturePoints = myPoints;
                My_Point point = myPoints[0];
                foreach (Card card in Card_List.CardList) {
                    if (card.Contain(point.CurrentPoint.Position))
                    {
                        Senders[1] = card;
                        card2 = card;
                        break;
                    }
                    else {
                        this.Senders = senders;
                    }
                }
                gestureEventArgs.Senders = this.Senders;
                this.Status = GESTURESTATUS.TERMINATE;
                OnTerminated(this, gestureEventArgs);
            }
        }

        public override void Fail()
        {
            Gesture_Event_Args gestureEventArgs = new Gesture_Event_Args();
            gestureEventArgs.GesturePoints = Points;
            gestureEventArgs.Senders = Senders;
            this.Status = GESTURESTATUS.FAIL;
            OnFailed(this, gestureEventArgs);
        }
    }
}
