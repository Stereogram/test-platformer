using System;
using SFML.Graphics;
using SFML.Window;

namespace testlol
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


    }
}
