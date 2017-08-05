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
            return AB.X * AC.Y - AB.Y * AC.X;
        }

        internal static Point Clamp(this Point p, Rectangle bounds)
        {
            p.X = (p.X < bounds.Left) ? bounds.Left : (p.X > (bounds.Left + bounds.Width - 1)) ? (bounds.Left + bounds.Width - 1) : p.X;
            p.Y = (p.Y < bounds.Top) ? bounds.Top : (p.Y > bounds.Top + bounds.Height - 1) ? (bounds.Top + bounds.Height - 1) : p.Y;
            return p;
        }

        internal static Rectangle Clamp(this Rectangle rect, Rectangle bounds)
        {
            int left = (rect.Left < bounds.Left) ? bounds.Left : (rect.Left > bounds.Right) ? bounds.Right : rect.Left;
            int top = (rect.Top < bounds.Top) ? bounds.Top : (rect.Top > bounds.Bottom) ? bounds.Bottom : rect.Top;
            int right = (rect.Right < bounds.Left) ? bounds.Left : (rect.Right > bounds.Right) ? bounds.Right : rect.Right;
            int bottom = (rect.Bottom < bounds.Top) ? bounds.Top : (rect.Bottom > bounds.Bottom) ? bounds.Bottom : rect.Bottom;
            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        internal static double Pythag(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        internal static double Pythag(PointD p1, PointD p2)
        {
            // Math.Pow is less performant
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

        internal static Point CenterPoint(this Point[] source)
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

        internal static Point CenterPoint(this Rectangle rect)
        {
            return new Point
            {
                X = rect.Width / 2 + rect.Left,
                Y = rect.Height / 2 + rect.Top
            };
        }

        internal static Rectangle ToRectangle(this Point[] pts) //fix rotation
        {
            if (pts.Length != 4)
                throw new ArgumentException("Length of Point Array must be 4");

            return new Rectangle
            {
                //prelim
                Width = pts[2].X - pts[0].X,
                Height = pts[2].Y - pts[0].Y,
                Location = pts[0]
            };
        }

        internal static Point[] ToPointArray(this Rectangle y)
        {
            Point[] x = new Point[4];
            x[0] = new Point(y.Left, y.Top);
            x[1] = new Point(y.Width + y.Left - 1, y.Top);
            x[2] = new Point(y.Width + y.Left - 1, y.Top + y.Height - 1);
            x[3] = new Point(y.Left, y.Top + y.Height - 1);
            return x;
        }
    }
}
