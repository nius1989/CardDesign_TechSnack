using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CardDesign
{
    class Calculator
    {
        public static double CalDistance(TouchPoint a, TouchPoint b)
        {
            return Math.Sqrt(Math.Pow((a.Position.X - b.Position.X), 2) + Math.Pow((a.Position.Y - b.Position.Y), 2));
        }
        public static double CalDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
        public static bool Include(Point[] Newpoint, double testx, double testy)
        {
            int i, j;
            bool c = false;
            for (i = 0, j = 3; i < 4; j = i++)
            {
                if (((Newpoint[i].Y > testy) != (Newpoint[j].Y > testy)) &&
               (testx < (Newpoint[j].X - Newpoint[i].X) * (testy - Newpoint[i].Y) / (Newpoint[j].Y - Newpoint[i].Y) + Newpoint[i].X))
                    c = !c;
            }
            return c;
        }

        public static bool Include(My_Point[] Newpoint, double testx, double testy)
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

        public static bool DoLinesIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }
        // Given three colinear points p, q, r, the function checks if
        // point q lies on line segment 'pr'
        private static bool onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;
            return false;
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        private static int orientation(Point p, Point q, Point r)
        {
            // See 10th slides from following link for derivation of the formula
            double val = (q.Y - p.Y) * (r.X - q.X)- (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  // colinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }
    }
}
