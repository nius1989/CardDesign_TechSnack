using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CardDesign
{
    public class My_Point
    {
        int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        TouchPoint startPoint;

        public TouchPoint StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        TouchPoint currentPoint;

        public TouchPoint CurrentPoint
        {
            get { return currentPoint; }
            set { currentPoint = value; }
        }

        DateTime startTime;

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        double life;

        public double Life
        {
            get { return life; }
            set { life = value; }
        }

        bool isLive = false;

        public bool IsLive
        {
            get { return isLive; }
            set { isLive = value; }
        }

        object sender;

        public object Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        Type senderType;

        public Type SenderType
        {
            get { return senderType; }
            set { senderType = value; }
        }

        public My_Point(int id, object sdr, Type t, TouchPoint touchPoint, DateTime dateTime)
        {
            this.iD = id;
            startPoint = touchPoint;
            currentPoint = touchPoint;
            startTime = dateTime;
            life = 0;
            isLive = true;
            sender = sdr;
            senderType = t;
        }

        public void UpdateMyPoint(TouchPoint touchPoint,DateTime dateTime)
        {
            TimeSpan ts = dateTime - startTime;
            if (ts.TotalMilliseconds - life > STATICS.POINT_REFRESH_RATE)
            {
                life = ts.TotalMilliseconds;
                currentPoint = touchPoint;
            }
        }

        public void KillMyPoint(DateTime dateTime)
        {
            TimeSpan ts = dateTime - startTime;
            life = ts.TotalMilliseconds;
            isLive = false;
        }
    }
}
