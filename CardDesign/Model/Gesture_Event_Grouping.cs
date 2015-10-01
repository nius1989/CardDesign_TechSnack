using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CardDesign
{
    public class Gesture_Event_Grouping:Gesture_Event
    {
        protected List<Card> cards = new List<Card>();
        Point[] previousPoint = new Point[4];
        Vector vector = new Vector();
        Vector vectorDistance = new Vector();

        public Vector Vector
        {
            get { return vector; }
        }
        public bool IsGrouping() {
            return vectorDistance.Length < STATICS.MIN_DISTANCE_FOR_MOVE;
        }
        public override void Register(object[] senders, My_Point[] myPoints)
        {
            if (myPoints != null && senders != null)
            {
                Points = myPoints;
                Senders = senders;
                Gesture_Event_Args gestureEventArgs = new Gesture_Event_Args();
                gestureEventArgs.GesturePoints = myPoints;
                gestureEventArgs.Senders = senders;
                this.Status = GESTURESTATUS.REGISTER;
                for (int i = 0; i < 4; i++)
                {
                    previousPoint[i] = new Point(myPoints[i].CurrentPoint.Position.X, myPoints[i].CurrentPoint.Position.Y);
                }
                OnRegistered(this, gestureEventArgs);
            }
        }
        public override void Continue(object[] senders, My_Point[] myPoints)
        {
            if (myPoints != null && senders != null && checkValid(null, null))
            {
                Gesture_Event_Args gestureEventArgs = new Gesture_Event_Args();
                gestureEventArgs.GesturePoints = myPoints;
                gestureEventArgs.Senders = senders;
                this.Status = GESTURESTATUS.CONTINUE;
                double xAvg = 0;
                double yAvg = 0;
                for (int i = 0; i < 4; i++) {
                    xAvg += myPoints[i].CurrentPoint.Position.X - previousPoint[i].X;
                    yAvg += myPoints[i].CurrentPoint.Position.Y - previousPoint[i].Y;
                    previousPoint[i] = new Point(myPoints[i].CurrentPoint.Position.X, myPoints[i].CurrentPoint.Position.Y);
                }
                xAvg = xAvg / 4;
                yAvg = yAvg / 4;
                vector = new Vector(xAvg, yAvg);
                vectorDistance.X += xAvg;
                vectorDistance.Y += yAvg;
                OnContinued(this, gestureEventArgs);
            }
            if (!checkValid(null,null))
            {
                Fail();
                return;
            }
            if (checkTerminate(null, null))
            {
                Terminate(senders, myPoints);
            }
        }
        protected override bool checkValid(object[] senders, My_Point[] myPoints)
        {
            return true;
        }
        protected override bool checkTerminate(object[] senders, My_Point[] myPoints)
        {
            return !Points[0].IsLive || !Points[1].IsLive || !Points[2].IsLive || !Points[3].IsLive;
        }
        public override void Terminate(object[] senders, My_Point[] myPoints)
        {
            if (myPoints != null && senders != null)
            {
                Gesture_Event_Args gestureEventArgs = new Gesture_Event_Args();
                gestureEventArgs.GesturePoints = myPoints;
                gestureEventArgs.Senders = senders;
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
