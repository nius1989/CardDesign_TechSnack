using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardDesign
{
    public enum GESTURESTATUS { DEFAULT, REGISTER, CONTINUE, TERMINATE, FAIL }
    public delegate void RegisteredEventHandler(object sender, Gesture_Event_Args gEventArgs);
    public delegate void ContinuedEventHandler(object sender, Gesture_Event_Args gEventArgs);
    public delegate void TerminatedEventHandler(object sender, Gesture_Event_Args gEventArgs);
    public delegate void FailEventHandler(object sender, Gesture_Event_Args gEventArgs);
    public class Gesture_Event
    {
        private GESTURESTATUS status = GESTURESTATUS.DEFAULT;

        public GESTURESTATUS Status
        {
            get { return status; }
            set { status = value; }
        }

        private object[] senders;

        public object[] Senders
        {
            get { return senders; }
            set { senders = value; }
        }
        private My_Point[] points;

        public My_Point[] Points
        {
            get { return points; }
            set{points = value;}
        }
        public event RegisteredEventHandler Registered;
        public event ContinuedEventHandler Continued;
        public event TerminatedEventHandler Terminated;
        public event FailEventHandler Failed;

        public bool ContainPoint(My_Point point) {
            return points.Contains(point);
        }
        protected virtual bool checkValid(object[] senders, My_Point[] myPoints)
        {
            //TO DO check if the the gesture is still valid
            return true;
        }
        protected virtual bool checkTerminate(object[] senders, My_Point[] myPoints)
        {
            return false;
        }
        //senders: UI elements associated with the gesture, myPoints: Points associated with the gesture
        public virtual void Register(object[] senders, My_Point[] myPoints){}
        public virtual void Continue(object[] senders, My_Point[] myPoints){}
        public virtual void Terminate(object[] senders, My_Point[] myPoints){}
        public virtual void Fail(){}
        //sender: the gesture itself, gEventArgs: gesture parameters
        protected virtual void OnRegistered(object sender, Gesture_Event_Args gEventArgs) {
            Registered(sender, gEventArgs);
        }

        protected virtual void OnContinued(object sender, Gesture_Event_Args gEventArgs){
            Continued(sender, gEventArgs);
        }
        protected virtual void OnTerminated(object sender, Gesture_Event_Args gEventArgs){
            foreach (My_Point point in gEventArgs.GesturePoints)
            {
                Point_List.ReleasePoint(point.ID);
            }
            Terminated(sender, gEventArgs);
        }
        protected virtual void OnFailed(object sender, Gesture_Event_Args gEventArgs){
            Failed(sender, gEventArgs);
        }
    }
}
