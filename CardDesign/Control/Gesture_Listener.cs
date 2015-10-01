using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    /// <summary>
    /// The base class for gesture listener. Link the listener to the gesture event.
    /// All Gesture_XXX_Listener should extend this class, and override the construct method
    /// </summary>
    class Gesture_Listener
    {

        protected Gesture_Controler gestureControler = null;
        protected RegisteredEventHandler registerHandler;
        protected ContinuedEventHandler continueHandler;
        protected TerminatedEventHandler terminateHandler;
        protected FailEventHandler failHandler;
        protected Gesture_Event gestureEvent;
        public Gesture_Listener(Gesture_Controler gestureControler,  Gesture_Event gesture)
        {
            registerHandler = new RegisteredEventHandler(RegisterGesture);
            continueHandler = new ContinuedEventHandler(ContinueGesture);
            terminateHandler = new TerminatedEventHandler(TerminateGesture);
            failHandler = new FailEventHandler(FailGesture);
            this.gestureEvent = gesture;
            this.gestureEvent.Registered += registerHandler;
            this.gestureEvent.Continued += continueHandler;
            this.gestureEvent.Terminated += terminateHandler;
            this.gestureEvent.Failed += failHandler;
            this.gestureControler = gestureControler;
        }
        public virtual void TerminateGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            gestureEvent.Registered -= registerHandler;
            gestureEvent.Continued -= continueHandler;
            gestureEvent.Terminated -= terminateHandler;
            gestureEvent.Failed -= failHandler;

            Record_Helper.PrintRecord(gestureControler.MainWindow, gestureEvent, gEventArgs, GESTURESTATUS.TERMINATE);
        }

        public virtual void ContinueGesture(object sender, Gesture_Event_Args gEventArgs)
        {
        }

        public virtual void RegisterGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            Record_Helper.PrintRecord(gestureControler.MainWindow, gestureEvent, gEventArgs, GESTURESTATUS.REGISTER);
        }
        public virtual void FailGesture(object sender, Gesture_Event_Args gEventArgs)
        {
            gestureEvent.Registered -= registerHandler;
            gestureEvent.Continued -= continueHandler;
            gestureEvent.Terminated -= terminateHandler;
            gestureEvent.Failed -= failHandler;
            Record_Helper.PrintRecord(gestureControler.MainWindow, gestureEvent, gEventArgs, GESTURESTATUS.FAIL);
        }
    }
}
