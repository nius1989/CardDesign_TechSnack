 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace CardDesign
{
    /// <summary>
    /// The controller class to detect the gesture.
    /// Fire a new thread.
    /// Periodically update all the gestures in the Gesture_List
    /// Periodically remove all the gestures which are terminated from the Gesture_List
    /// Periodically check the My_Point list
    /// If one or more points are detected as a gesture, put create a Gesture_Event variable and add it to the Gesture_List. Then remove these points from the list
    /// </summary>
    public class Gesture_Controler
    {
        Thread gestureDetectionThread = null;
        bool isRunning = false;
        MainWindow mainWindow;
        Sorting_Group_Controler groupingControler;

        internal Sorting_Group_Controler GroupingControler
        {
            get { return groupingControler; }
            set { groupingControler = value; }
        }
        public MainWindow MainWindow
        {
            get { return mainWindow; }
            set { mainWindow = value; }
        }
        List<My_Point> newGesturePoints = new List<My_Point>();

        public Gesture_Controler(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            groupingControler = new Sorting_Group_Controler(this);
        }

        public void start()
        {
            if (gestureDetectionThread != null)
            {
                isRunning = false;
                Thread.Sleep(100);
                gestureDetectionThread.Abort();
            }
            gestureDetectionThread = new Thread(new ThreadStart(processGesture));
            gestureDetectionThread.SetApartmentState(ApartmentState.STA);
            isRunning = true;
            gestureDetectionThread.Start();
        }

        public void quit() {
            if (gestureDetectionThread != null)
            {
                isRunning = false;
                Thread.Sleep(100);
                gestureDetectionThread.Abort();
                gestureDetectionThread = null;
            }
        }
        private void processGesture()
        {
            while (isRunning)
            {
                if (Touch_Controler.isTouched())
                {
                    Thread.Sleep(STATICS.GESTURE_REFRESH_RATE);
                    updateGesture();
                    terminateGesture();
                    detectGesture();
                }
            }
        }

        private void updateGesture()
        {
            newGesturePoints.Clear();
            My_Point[] myPoints = new My_Point[Point_List.TouchPointList.Count];
            myPoints = Point_List.TouchPointList.Values.ToArray();
            foreach (My_Point point in myPoints)
            {
                if (point.Life > STATICS.MIN_GESTURE_LIFE)
                {
                    bool inUse = false;
                    Gesture_Event[] gestures = Gesture_List.GestureList.ToArray();
                    foreach (Gesture_Event gesture in gestures)
                    {
                        if ((gesture.Status == GESTURESTATUS.CONTINUE || gesture.Status == GESTURESTATUS.REGISTER) && gesture.ContainPoint(point))
                        {
                            inUse = true;
                            break;
                        }
                    }
                    if (!inUse)
                    {
                        newGesturePoints.Add(point);
                    }
                }
            }
            lock (Gesture_List.GestureList)
            {
                foreach (Gesture_Event gesture in Gesture_List.GestureList)
                {
                    gesture.Continue(gesture.Senders, gesture.Points);
                }
            }
        }

        private void terminateGesture()
        {
            List<Gesture_Event> waitToRemove = new List<Gesture_Event>();
            lock (Gesture_List.GestureList)
            {
                foreach (Gesture_Event gesture in Gesture_List.GestureList)
                {
                    if (gesture.Status == GESTURESTATUS.TERMINATE || gesture.Status == GESTURESTATUS.FAIL)
                    {
                        waitToRemove.Add(gesture);
                    }
                }

                foreach (Gesture_Event gesture in waitToRemove)
                {
                    foreach (My_Point point in gesture.Points)
                    {
                        mainWindow.GestureIndicatorLayer.Remove(point.ID);
                        Gesture_List.removeGesture(gesture);
                    }
                }
            }
            waitToRemove.Clear();
        }



        private void detectGesture()
        {
            if (newGesturePoints.Count > 0)
            {
                //For Lab
                //detectLinkingRegister(newGesturePoints);
                //detectCopyingRegister(newGesturePoints);
                detectSortingRegister(newGesturePoints);
                //detectEmphasizingRegister(newGesturePoints);
                //detectGroupingRegister(newGesturePoints);
                //detectSortingGroupResgister(newGesturePoints);
                //detectShowingRegister(newGesturePoints);
                detectShowingSortingRegister(newGesturePoints);
                detectCuttingSortingRegister(newGesturePoints);
                //detectCuttingLinkRegister(newGesturePoints);
            }
        }




        private bool pnpoly(My_Point[] Newpoint, double testx, double testy)
        {
            int i, j;
            bool c = false;
            for (i = 0, j = 3; i < 4; j = i++)
            {
                if (((Newpoint[i].CurrentPoint.Position.Y > testy) != (Newpoint[j].CurrentPoint.Position.Y > testy)) &&
               (testx < (Newpoint[j].CurrentPoint.Position.X - Newpoint[i].CurrentPoint.Position.X) * (testy - Newpoint[i].CurrentPoint.Position.Y) / (Newpoint[j].CurrentPoint.Position.Y - Newpoint[i].CurrentPoint.Position.Y) + Newpoint[i].CurrentPoint.Position.X))
                    c = !c;
            }
            return c;
        }
        private Gesture_Event_Showing detectShowingRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            if (points.Count > 0)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].Life > STATICS.MIN_SHOW_LIFE && points[i].Sender is Card)
                    {
                        result.Add(points[i]);
                        for (int m = 0; m < points.Count; m++)
                        {
                            if (i != m && points[i].Sender == points[m].Sender && points[m].Life > STATICS.MIN_SHOW_LIFE)
                            {
                                result.Add(points[m]);
                            }
                        }
                        if (result.Count > 2)
                        {
                            My_Point[] argPoints = result.ToArray();
                            object[] objects = new object[1];
                            objects[0] = argPoints[0].Sender;
                            Gesture_Event_Showing showEvent = new Gesture_Event_Showing();
                            showEvent.Points = argPoints;
                            Gesture_List.addGesture(showEvent);
                            Gesture_Showing_Listener gestureListener = new Gesture_Showing_Listener(this, showEvent);
                            showEvent.Register(objects, argPoints);
                            return showEvent;
                        }
                        else {
                            result.Clear();
                        }
                    }
                }
            }
            return null;
        }
        private Gesture_Event_Linking detectLinkingRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            foreach (My_Point point in points)
            {
                if (point.Sender is Linking_Icon)
                {
                    result.Add(point);
                    Card card = (point.Sender as Linking_Icon).Card; 
                    My_Point[] argPoints = result.ToArray();
                    object[] objects = new object[2];
                    objects[0] = card;
                    Gesture_Event_Linking linkEvent = new Gesture_Event_Linking();
                    linkEvent.Points = argPoints;
                    Gesture_List.addGesture(linkEvent);
                    Gesture_Linking_Listener gestureLinkingListener = new Gesture_Linking_Listener(this, linkEvent);
                    linkEvent.Register(objects, argPoints);
                    foreach (My_Point p in result)
                    {
                        newGesturePoints.Remove(p);
                    }
                    return linkEvent;
                }
            }
            return null;
        }

        private Gesture_Event_Copying detectCopyingRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            foreach (My_Point point in points)
            {
                if (point.Sender is Copy_Icon)
                {
                    result.Add(point);
                    Card card = (point.Sender as Copy_Icon).Card;
                    My_Point[] argPoints = result.ToArray();
                    object[] objects = new object[2];
                    objects[0] = card;
                    Gesture_Event_Copying copyEvent = new Gesture_Event_Copying();
                    copyEvent.Points = argPoints;
                    Gesture_List.addGesture(copyEvent);
                    Gesture_Copying_Listener gestureLinkingListener = new Gesture_Copying_Listener(this, copyEvent);
                    copyEvent.Register(objects, argPoints);
                    foreach (My_Point p in result)
                    {
                        newGesturePoints.Remove(p);
                    }
                    return copyEvent;
                }
            }
            return null;
        }
        
        private Gesture_Event_Emphasizing detectEmphasizingRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            Gesture_Event_Emphasizing emphasizeEvent=null;
            if (points.Count > 0)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (Calculator.CalDistance(points[i].StartPoint, points[i].CurrentPoint) < STATICS.MIN_DISTANCE_FOR_MOVE && points[i].Life > STATICS.MIN_LONG_PRESS_LIFE && points[i].Sender is Card)
                    {
                        bool moreThan1 = false;
                        if (points.Count > 1)
                        {
                            for (int m = 0; m < points.Count; m++)
                            {
                                if (i != m && points[i].Sender == points[m].Sender)
                                {
                                    moreThan1 = true;
                                    break;
                                }
                            }
                        }
                        if (!moreThan1)
                        {
                            result.Add(points[i]);
                            My_Point[] argPoints = result.ToArray();
                            object[] objects = new object[argPoints.Length];
                            objects[0] = argPoints[0].Sender;
                            emphasizeEvent = new Gesture_Event_Emphasizing();
                            emphasizeEvent.Points = argPoints;
                            Gesture_List.addGesture(emphasizeEvent);
                            Gesture_Emphasizing_Listener gestureListener = new Gesture_Emphasizing_Listener(this, emphasizeEvent);
                            emphasizeEvent.Register(objects, argPoints);
                            foreach (My_Point p in result)
                            {
                                points.Remove(p);
                            }
                            return emphasizeEvent;
                        }
                    }
                }
            }
            return null;
        }
        private Gesture_Event_Grouping detectGroupingRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            if (points.Count == 4)
            {
                foreach (My_Point p in points)
                {
                    if(p.Sender is Card_Layer)
                        result.Add(p);
                }
            }
            if (result.Count ==4)
            {
                My_Point[] argPoints = result.ToArray();
                Card[] cards = Card_List.CardList.ToArray();
                My_Point[] Newpoint = new My_Point[4];
                for (int i = 0; i < 4; i++)
                {
                    Newpoint[i] = argPoints[i];
                }
                double maxX_1 = Math.Max(Math.Min(Newpoint[0].CurrentPoint.Position.X, Newpoint[1].CurrentPoint.Position.X), Math.Min(Newpoint[2].CurrentPoint.Position.X, Newpoint[3].CurrentPoint.Position.X));
                double maxX_2 = Math.Min(Math.Max(Newpoint[0].CurrentPoint.Position.X, Newpoint[1].CurrentPoint.Position.X), Math.Max(Newpoint[2].CurrentPoint.Position.X, Newpoint[3].CurrentPoint.Position.X));
                double maxY_1 = Math.Max(Math.Min(Newpoint[0].CurrentPoint.Position.Y, Newpoint[1].CurrentPoint.Position.Y), Math.Min(Newpoint[2].CurrentPoint.Position.Y, Newpoint[3].CurrentPoint.Position.Y));
                double maxY_2 = Math.Min(Math.Max(Newpoint[0].CurrentPoint.Position.Y, Newpoint[1].CurrentPoint.Position.Y), Math.Max(Newpoint[2].CurrentPoint.Position.Y, Newpoint[3].CurrentPoint.Position.Y));
                double midX = (maxX_1 + maxX_2) / 2;
                double midY = (maxY_1 + maxY_2) / 2;

                My_Point[] orderedPoints = new My_Point[4];
                int count1 = 0;
                int count2 = 0;
                int count3 = 0;
                int count4 = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (Newpoint[i].CurrentPoint.Position.X > midX && Newpoint[i].CurrentPoint.Position.Y <= midY) { count1++; }
                    if (Newpoint[i].CurrentPoint.Position.X <= midX && Newpoint[i].CurrentPoint.Position.Y < midY) { count2++; }
                    if (Newpoint[i].CurrentPoint.Position.X < midX && Newpoint[i].CurrentPoint.Position.Y >= midY) { count3++; }
                    if (Newpoint[i].CurrentPoint.Position.X >= midX && Newpoint[i].CurrentPoint.Position.Y > midY) { count4++; }

                }
                if (count1 > 1 || count2 > 1 || count3 > 1 || count4 > 1)
                {
                    if (count1 > 1)
                    {
                        int countIndexRight1 = 0;
                        int countIndexRight2 = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (Newpoint[i].CurrentPoint.Position.X > midX && Newpoint[i].CurrentPoint.Position.Y <= midY)
                            {
                                countIndexRight1++;
                                if (countIndexRight1 > 1)
                                {
                                    if (Newpoint[i].CurrentPoint.Position.X < orderedPoints[0].CurrentPoint.Position.X)
                                    {
                                        orderedPoints[1] = argPoints[i];
                                    }
                                    else
                                    {
                                        orderedPoints[1] = orderedPoints[0];
                                        orderedPoints[0] = argPoints[i];
                                    }
                                    break;
                                }
                                orderedPoints[0] = argPoints[i];
                            }
                        }
                        double disRight = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (Newpoint[i].CurrentPoint.Position.X < midX && Newpoint[i].CurrentPoint.Position.Y >= midY)
                            {

                                countIndexRight2++;
                                if (countIndexRight2 > 1)
                                {
                                    double temp = Math.Pow((Newpoint[i].CurrentPoint.Position.X - orderedPoints[1].CurrentPoint.Position.X), 2) + Math.Pow((Newpoint[i].CurrentPoint.Position.Y - orderedPoints[1].CurrentPoint.Position.Y), 2);
                                    if (temp > disRight)
                                    {
                                        orderedPoints[3] = argPoints[i];
                                    }
                                    else
                                    {
                                        orderedPoints[3] = orderedPoints[2];
                                        orderedPoints[2] = argPoints[i];
                                    }
                                    break;
                                }
                                orderedPoints[2] = argPoints[i];
                                disRight = Math.Pow((Newpoint[i].CurrentPoint.Position.X - orderedPoints[1].CurrentPoint.Position.X), 2) + Math.Pow((Newpoint[i].CurrentPoint.Position.Y - orderedPoints[1].CurrentPoint.Position.Y), 2);
                            }
                        }
                    }
                    else if (count2 > 1)
                    {
                        int countIndexLeft1 = 0;
                        int countIndexLeft2 = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (Newpoint[i].CurrentPoint.Position.X <= midX && Newpoint[i].CurrentPoint.Position.Y < midY)
                            {

                                countIndexLeft1++;
                                if (countIndexLeft1 > 1)
                                {
                                    if (Newpoint[i].CurrentPoint.Position.X < orderedPoints[0].CurrentPoint.Position.X)
                                    {
                                        orderedPoints[1] = argPoints[i];
                                    }
                                    else
                                    {
                                        orderedPoints[1] = orderedPoints[0];
                                        orderedPoints[0] = argPoints[i];
                                    }
                                    break;
                                }
                                orderedPoints[0] = argPoints[i];
                            }
                        }
                        double disLeft = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (Newpoint[i].CurrentPoint.Position.X >= midX && Newpoint[i].CurrentPoint.Position.Y > midY)
                            {

                                countIndexLeft2++;
                                if (countIndexLeft2 > 1)
                                {
                                    double temp = Math.Pow((Newpoint[i].CurrentPoint.Position.X - orderedPoints[1].CurrentPoint.Position.X), 2) + Math.Pow((Newpoint[i].CurrentPoint.Position.Y - orderedPoints[1].CurrentPoint.Position.Y), 2);
                                    if (temp > disLeft)
                                    {
                                        orderedPoints[3] = argPoints[i];
                                    }
                                    else
                                    {
                                        orderedPoints[3] = orderedPoints[2];
                                        orderedPoints[2] = argPoints[i];
                                    }
                                    break;
                                }
                                orderedPoints[2] = argPoints[i];
                                disLeft = Math.Pow((Newpoint[i].CurrentPoint.Position.X - orderedPoints[1].CurrentPoint.Position.X), 2) + Math.Pow((Newpoint[i].CurrentPoint.Position.Y - orderedPoints[1].CurrentPoint.Position.Y), 2);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (Newpoint[i].CurrentPoint.Position.X > midX && Newpoint[i].CurrentPoint.Position.Y < midY)
                        {
                            orderedPoints[0] = argPoints[i];
                        }
                        else if (Newpoint[i].CurrentPoint.Position.X < midX && Newpoint[i].CurrentPoint.Position.Y < midY)
                        {
                            orderedPoints[1] = argPoints[i];
                        }
                        else if (Newpoint[i].CurrentPoint.Position.X < midX && Newpoint[i].CurrentPoint.Position.Y > midY)
                        {
                            orderedPoints[2] = argPoints[i];
                        }
                        else if (Newpoint[i].CurrentPoint.Position.X > midX && Newpoint[i].CurrentPoint.Position.Y > midY)
                        {
                            orderedPoints[3] = argPoints[i];
                        }
                    }
                }
                List<Card> objects = new List<Card>();
                lock (cards)
                {
                    foreach (Card c in cards)
                    {
                        if (pnpoly(orderedPoints, c.CurrentPosition.X, c.CurrentPosition.Y))
                        {
                            objects.Add(c);
                        }
                    }
                }

                if (objects.Count > 0)
                {
                    Gesture_Event_Grouping gestureEvent = new Gesture_Event_Grouping();
                    gestureEvent.Points = orderedPoints;
                    gestureEvent.Senders = objects.ToArray();
                    Gesture_List.addGesture(gestureEvent);
                    Gesture_Grouping_Listener gestureListener = new Gesture_Grouping_Listener(this, gestureEvent);
                    gestureEvent.Register(objects.ToArray(), orderedPoints);
                    foreach (My_Point p in result) {
                        points.Remove(p);
                    }
                    return gestureEvent;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        private Gesture_Event_Sorting detectSortingRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            Gesture_Event_Sorting groupEvent=null;
            foreach (My_Point p in points) {
                if (!result.Contains(p) && p.Sender is Card && Calculator.CalDistance(p.StartPoint, p.CurrentPoint) >= STATICS.MIN_DISTANCE_FOR_MOVE)
                {
                    Card c=p.Sender as Card;
                    foreach (Menu_Sort_Button button in Group_List.GroupButtons.Values)
                    {
                        if (c.Contain(button.CurrentPosition))
                        {
                            foreach (My_Point p2 in points)
                            {
                                if (p.Sender == p2.Sender && !result.Contains(p2))
                                    result.Add(p2);
                            }
                            My_Point[] argPoints = result.ToArray();
                            object[] objects = new object[2];
                            objects[0] = c;
                            objects[1] = button;
                            groupEvent = new Gesture_Event_Sorting();
                            groupEvent.Points = argPoints;
                            Gesture_List.addGesture(groupEvent);
                            Gesture_Sorting_Listener gestureListener = new Gesture_Sorting_Listener(this, groupEvent);
                            groupEvent.Register(objects, argPoints);
                        }
                    }
                }
            }
            foreach (My_Point p in result) {
                points.Remove(p);
            }
            return groupEvent;
        }
        private Gesture_Event_SortingGroup detectSortingGroupResgister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            Gesture_Event_Grouping resturedCluster = null;
            Gesture_Event_SortingGroup sortingGroupEvent=null;
            foreach (My_Point p in points)
            {
                if (p.Sender is Menu_Sort_Button)
                {
                    foreach (Gesture_Event gesture in Gesture_List.GestureList)
                    {
                        if (gesture is Gesture_Event_Grouping)
                        {
                            Gesture_Event_Grouping cluster = gesture as Gesture_Event_Grouping;
                            if (cluster.IsGrouping())
                            {
                                My_Point[] clusterPoints = cluster.Points;
                                if (pnpoly(clusterPoints, p.CurrentPoint.Position.X, p.CurrentPoint.Position.Y))
                                {
                                    resturedCluster = cluster;
                                    result.Add(p);
                                    foreach (My_Point clusterPoint in clusterPoints)
                                    {
                                        result.Add(clusterPoint);
                                    }
                                    My_Point[] argPoints = result.ToArray();
                                    object[] objects = new object[cluster.Senders.Length + 1];
                                    objects[0] = cluster.Points[0].Sender;
                                    for (int i = 0; i < cluster.Senders.Length; i++)
                                    {
                                        objects[i + 1] = cluster.Senders[i];
                                    }
                                    sortingGroupEvent = new Gesture_Event_SortingGroup();
                                    sortingGroupEvent.Points = cluster.Points;
                                    Gesture_List.addGesture(sortingGroupEvent);
                                    Gesture_SortingGroup_Listener listener = new Gesture_SortingGroup_Listener(this, sortingGroupEvent);
                                    sortingGroupEvent.Register(objects, argPoints);
                                }
                            }
                        }
                    }
                }
            }
            if (resturedCluster != null)
            {
                Gesture_List.removeGesture(resturedCluster);
                mainWindow.GroupingGestureLayer.Remove(resturedCluster);
                foreach (My_Point p in result) {
                    points.Remove(p);
                }
                return sortingGroupEvent;
            }
            return null;
        }
        private Gesture_Event_Showing_Sorting detectShowingSortingRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            Gesture_Event_Showing_Sorting showGroupEvent = null;
            if (points.Count > 0)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (Calculator.CalDistance(points[i].StartPoint, points[i].CurrentPoint) < STATICS.MIN_DISTANCE_FOR_MOVE && points[i].Life > STATICS.MIN_LONG_PRESS_LIFE && points[i].Sender is Menu_Sort_Button)
                    {
                        bool moreThan1 = false;
                        if (points.Count > 1)
                        {
                            for (int m = 0; m < points.Count; m++)
                            {
                                if (i != m && points[i].Sender == points[m].Sender)
                                {
                                    moreThan1 = true;
                                    break;
                                }
                            }
                        }
                        if (!moreThan1)
                        {
                            result.Add(points[i]);
                            My_Point[] argPoints = result.ToArray();
                            object[] objects = new object[argPoints.Length];
                            objects[0] = argPoints[0].Sender;
                            showGroupEvent = new Gesture_Event_Showing_Sorting();
                            showGroupEvent.Points = argPoints;
                            Gesture_List.addGesture(showGroupEvent);
                            Gesture_Showing_Sorting_Listener gestureListener = new Gesture_Showing_Sorting_Listener(this, showGroupEvent);
                            showGroupEvent.Register(objects, argPoints);
                            foreach (My_Point p in result)
                            {
                                points.Remove(p);
                            }
                            return showGroupEvent;
                        }
                    }
                }
            }
            return null;
        }
        private Gesture_Event_Cutting_Sorting detectCuttingSortingRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            Gesture_Event_Cutting_Sorting cuttingEvent = null;
            if (Group_List.CardGroups.Count > 0 && mainWindow.SortingGestureLayer.GroupLinks.Count > 0)
            {
                String[] keys = mainWindow.SortingGestureLayer.GroupLinks.Keys.ToArray();
                foreach (My_Point p in points)
                {
                    if (p.Sender is Card_Layer)
                    {
                        foreach (String key in keys)
                        {
                            Menu_Sort_Button button = Group_List.GroupButtons[key];
                            if (Group_List.CardGroups.ContainsKey(key))
                            {
                                Card[] cards = Group_List.CardGroups[key].ToArray();
                                foreach (Card c in cards)
                                {
                                    bool isIntersect = Calculator.DoLinesIntersect(new Point(button.CurrentPosition.X, button.CurrentPosition.Y),
                                        new Point(c.CurrentPosition.X, c.CurrentPosition.Y),
                                        new Point(p.StartPoint.Position.X, p.StartPoint.Position.Y),
                                        new Point(p.CurrentPoint.Position.X, p.CurrentPoint.Position.Y));
                                    if (isIntersect)
                                    {
                                        result.Add(p);
                                        My_Point[] argPoints = result.ToArray();
                                        object[] objects = new object[2];
                                        objects[0] = button;
                                        objects[1] = c;
                                        cuttingEvent = new Gesture_Event_Cutting_Sorting();
                                        cuttingEvent.Points = argPoints;
                                        Gesture_List.addGesture(cuttingEvent);
                                        Gesture_Cutting_Sorting_Listener gestureListener = new Gesture_Cutting_Sorting_Listener(this, cuttingEvent);
                                        cuttingEvent.Register(objects, argPoints);
                                        foreach (My_Point p2 in result)
                                        {
                                            points.Remove(p2);
                                        }
                                        return cuttingEvent;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
        private Gesture_Event_Cutting_Link detectCuttingLinkRegister(List<My_Point> points)
        {
            List<My_Point> result = new List<My_Point>();
            Gesture_Event_Cutting_Link cuttingEvent = null;
            if (Link_List.CardLinks.Count > 0 && mainWindow.LinkingGestureLayer.CardLinks.Count > 0)
            {
                Gesture_Event_Linking[] links = Link_List.CardLinks.ToArray();
                foreach (My_Point p in points)
                {
                    if (p.Sender is Card_Layer)
                    {
                        foreach (Gesture_Event_Linking link in links)
                        {

                            bool isIntersect = Calculator.DoLinesIntersect(new Point(link.Card1.CurrentPosition.X, link.Card1.CurrentPosition.Y),
                                new Point(link.Card2.CurrentPosition.X, link.Card2.CurrentPosition.Y),
                                new Point(p.StartPoint.Position.X, p.StartPoint.Position.Y),
                                new Point(p.CurrentPoint.Position.X, p.CurrentPoint.Position.Y));
                            if (isIntersect)
                            {
                                result.Add(p);
                                My_Point[] argPoints = result.ToArray();
                                object[] objects = new object[2];
                                objects[0] = link;
                                cuttingEvent = new Gesture_Event_Cutting_Link();
                                cuttingEvent.Points = argPoints;
                                Gesture_List.addGesture(cuttingEvent);
                                Gesture_Cutting_Link_Listener gestureListener = new Gesture_Cutting_Link_Listener(this, cuttingEvent);
                                cuttingEvent.Register(objects, argPoints);
                                foreach (My_Point p2 in result)
                                {
                                    points.Remove(p2);
                                }
                                return cuttingEvent;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
