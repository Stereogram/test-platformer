using System;
using SFML.Graphics;
using SFML.Window;

namespace testlol.Util
{
    public static class Extensions
    {
        public static double Length(this Vector2f a)
        {
            return Math.Sqrt( (a.X*a.X)+(a.Y*a.Y) );
        }

        public static double Length2(this Vector2f a)
        {
            return (a.X * a.X) + (a.Y * a.Y);
        }

        public static Vector2f Abs(this Vector2f a)
        {
            return new Vector2f(Math.Abs(a.X), Math.Abs(a.Y));
        }

        public static double Project(this Vector2f a, Vector2f b)
        {
            return Dot(a, b) / a.Length();
        }

        public static double Dot(this Vector2f a, Vector2f b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static int X(this int[,] a)
        {
            return a.GetLength(1);
        }

        public static int Y(this int[,] a)
        {
            return a.GetLength(0);
        }

        public static string Print(this int[,] a)
        {
            string r = "";
            for (int i = 0; i < a.Y(); i++)
            {
                for (int j = 0; j < a.X(); j++)
                {
                    r += a[i, j] + ",";
                }
                r += '\n';
            }
            return r;
        }

        public static float Bottom(this FloatRect a)
        {
            return a.Height + a.Top;
        }

        public static float Right(this FloatRect a)
        {
            return a.Width + a.Left;
        }

        public static RectangleShape ToRectangleShape(this FloatRect a)
        {
            return new RectangleShape(new Vector2f(a.Width, a.Height)) {Position = new Vector2f(a.Left, a.Top)};
        }


    }
}
