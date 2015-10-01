using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace CardDesign
{
    class Point_List
    {
        static Dictionary<int, My_Point> touchPointList = new Dictionary<int, My_Point>();

        public static Dictionary<int, My_Point> TouchPointList
        {
            get { return Point_List.touchPointList; }
        }


        static int listSize = 0;

        public static int ListSize
        {
            get { return listSize; }
        }

        public static void clear()
        {
            touchPointList.Clear();
            listSize = 0;
        }


        public static bool ContainPoint(TouchPoint p) {
            return touchPointList.ContainsKey(p.TouchDevice.Id);
        }
        public static My_Point GetPoint(TouchPoint p) {
            return touchPointList[p.TouchDevice.Id];
        }

        public static My_Point ReleasePoint(int pointIndex)
        {
            if (touchPointList.ContainsKey(pointIndex))
            {
                touchPointList[pointIndex].KillMyPoint(DateTime.Now);
                My_Point myPoint = touchPointList[pointIndex];
                myPoint.IsLive = false;
                touchPointList.Remove(pointIndex);
                listSize--;
                return myPoint;
            }
            else
            {
                return null;
            }
        }

        public static bool AddPoint(object sdr, Type t, int touchDevice, TouchPoint touchPoint)
        {
            bool isSuccess = false;
            if (touchPointList.ContainsKey(touchDevice))
            {
                touchPointList[touchDevice].UpdateMyPoint(touchPoint, DateTime.Now);
            }
            else
            {
                if (touchPointList.Count < STATICS.MAX_TOUCH_POINT)
                {
                    int senderCount = 0;
                    if (sdr is Card)
                    {
                        lock (touchPointList)
                        {
                            foreach (My_Point p in touchPointList.Values)
                            {
                                if (p.Sender == sdr)
                                {
                                    senderCount++;
                                }
                            }
                        }
                    }
                    if (senderCount < (STATICS.MAX_TOUCH_POINT / STATICS.USER_NUMER))
                    {
                        My_Point newPoint = new My_Point(touchDevice, sdr, t, touchPoint, DateTime.Now);
                        newPoint.IsLive = true;
                        touchPointList.Add(touchDevice, newPoint);
                        listSize++;
                    }
                }
            }
            isSuccess = true;
            return isSuccess;
        }

        public static My_Point UpdatePoint(object sdr, Type t, int touchDevice, TouchPoint touchPoint)
        {
            lock (touchPointList)
            {
                if (touchPointList.ContainsKey(touchDevice))
                {
                    touchPointList[touchDevice].UpdateMyPoint(touchPoint, DateTime.Now);
                    if (touchPointList.ContainsKey(touchDevice))
                    {
                        return touchPointList[touchDevice];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
