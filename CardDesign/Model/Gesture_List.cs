using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    class Gesture_List
    {

        static List<Gesture_Event> gestureList = new List<Gesture_Event>();

        internal static List<Gesture_Event> GestureList
        {
            get { return Gesture_List.gestureList; }
        }
        
        public static void addGesture(Gesture_Event gestureEvent){
            if (!gestureList.Contains(gestureEvent))
            {
                gestureList.Add(gestureEvent);
            }
        }

        public static void removeGesture(Gesture_Event gestureEvent) 
        {
            if (gestureList.Contains(gestureEvent))
            {
                gestureList.Remove(gestureEvent);
            }
        }
    }
}
