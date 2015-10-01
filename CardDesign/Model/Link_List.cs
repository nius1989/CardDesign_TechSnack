using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace CardDesign
{
    class Link_List
    {
        static List<Gesture_Event_Linking> cardLinks = new List<Gesture_Event_Linking>();

        public static List<Gesture_Event_Linking> CardLinks
        {
            get { return Link_List.cardLinks; }
        }

        public static void RemoveLink(Gesture_Event_Linking link) {
            if (cardLinks.Contains(link))
            {
                cardLinks.Remove(link);
            }
        }
        public static void AddLink(Gesture_Event_Linking link) {
            if (!cardLinks.Contains(link))
            {
                cardLinks.Add(link);
            }
        }
    }
}
