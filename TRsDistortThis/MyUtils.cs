using System;
using System.Drawing;

namespace TRsDistortThis
{
    struct PointD
    {
        internal double X { get; set; }
        internal double Y { get; set; }

        internal PointD(double p1, double p2)
        {
            X = p1;
            Y = p2;
        }
    }

    static class MyUtils
    {
        internal static double xproduct(PointD A, PointD B, PointD C)
        {
            PointD AB = new PointD(B.X - A.X, B.Y - A.Y);
            PointD AC = new PointD(C.X - A.X, C.Y - A.Y);
            double cross = AB.X * AC.Y - AB.Y * AC.X;
            return cross;
        }


        internal static int clamp(int p, int l, int h)
        {
            p = (p < l) ? l : (p > h) ? h : p;

            return p;
        }

        internal static float clampF(float p, float l, float h)
        {
            p = (p < l) ? l : (p > h) ? h : p;

            return p;
        }

        internal static double clampD(double p, double l, double h)
        {
            p = (p < l) ? l : (p > h) ? h : p;

            return p;
        }

        internal static Point clampP(Point p, Rectangle r)
        {
            p.X = (p.X < r.Left) ? r.Left : (p.X > (r.Left + r.Width - 1)) ? (r.Left + r.Width - 1) : p.X;
            p.Y = (p.Y < r.Top) ? r.Top : (p.Y > r.Top + r.Height - 1) ? (r.Top + r.Height - 1) : p.Y;
            return p;
        }

        internal static Rectangle clampR(Rectangle p, Rectangle r)
        {
            int left = (p.X < r.Left) ? r.Left : (p.X > r.Right) ? r.Right : p.X;
            int top = (p.Y < r.Top) ? r.Top : (p.Y > r.Bottom) ? r.Bottom : p.Y;
            int right = (p.Right < r.Left) ? r.Left : (p.Right > r.Right) ? r.Right : p.Right;
            int bottom = (p.Bottom < r.Top) ? r.Top : (p.Bottom > r.Bottom) ? r.Bottom : p.Bottom;
            return new Rectangle(left, top, right - left, bottom - top);
        }

        internal static double Pythag(double p1, double p2)
        {
            return Math.Sqrt(p1 * p1 + p2 * p2);
        }

        internal static double Pythag(Point p1, Point p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

        internal static double Pythag(PointD p1, PointD p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

        internal static Point Centroid(Point[] source)
        {
            Point tmp = new Point();
            if (source.Length == 0) return tmp;
            foreach (Point i in source)
            {
                tmp.X += i.X;
                tmp.Y += i.Y;
            }
            tmp.X /= source.Length;
            tmp.Y /= source.Length;
            return tmp;
        }

    }
}
