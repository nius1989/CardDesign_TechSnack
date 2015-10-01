using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesign
{
    public class Record_Helper
    {
        public class RecordObj {
            public String time_stemp;
            public String gesture_name;
            public String status;
            public String[] cards;
            public String[] buttons;
            public String[] points;
        }
        public static String PrintRecord(MainWindow mainWindow, Gesture_Event gesture, Gesture_Event_Args args, GESTURESTATUS status) {
            String oneRecord = "";
            if (STATICS.DEBUG_MODE)
            {
                RecordObj rObj = new RecordObj();
                oneRecord += DateTime.Now.ToString("h:mm:ss:fff tt");
                rObj.time_stemp = DateTime.Now.ToString("h:mm:ss:fff tt");

                oneRecord += "$"+gesture.GetType().Name.ToString();
                rObj.gesture_name=gesture.GetType().Name.ToString();

                oneRecord += "$" + status.ToString();
                rObj.status = status.ToString();

                List<String> cards = new List<string>();
                List<String> buttons = new List<string>();
                foreach (object obj in args.Senders) {
                    if (obj is Card) {
                        Card c = obj as Card;
                        oneRecord += "$" + "Card={" + c.Owner + "," + c.UID + "," + c.CardText + "}";
                        cards.Add(c.Owner + "," + c.UID + "," + c.CardText);
                    }
                    else if (obj is Menu_Sort_Button) {
                        Menu_Sort_Button b = obj as Menu_Sort_Button;
                        oneRecord += "$" + "Button={" + b.GroupID + "," + b.GroupText + "}";
                        buttons.Add(b.GroupID + "," + b.GroupText);
                    }
                }
                rObj.cards = cards.ToArray();
                rObj.buttons = buttons.ToArray();

                List<String> points = new List<string>();
                int index = 0;
                foreach (My_Point point in args.GesturePoints) {
                    oneRecord += "$" + "Point={" + index + "," + "[" + point.StartTime.ToString("h:mm:ss:fff tt") +","+ point.StartPoint.Position.ToString() + "]"
                        + "," + "[" +DateTime.Now.ToString("h:mm:ss:fff tt")+","+ point.CurrentPoint.Position.ToString() + "]" + "}";
                    points.Add(point.StartTime.ToString("h:mm:ss:fff tt") + "," + point.StartPoint.Position.ToString()+","+
                        DateTime.Now.ToString("h:mm:ss:fff tt") + "," + point.CurrentPoint.Position.ToString());
                    index++;
                }
                rObj.points = points.ToArray();
                mainWindow.ControlWindow.SaveRecord(rObj);
                mainWindow.ControlWindow.UpdateTextInfo(oneRecord, 2);
            }
            return oneRecord;
        }
    }
}
