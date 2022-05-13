using System;

namespace Assets.Scripts.Model
{
    public class Vector2Integer: IEquatable<Vector2Integer>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2Integer(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Vector2Integer other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", this.X, this.Y);
        }

        public static Vector2Integer operator +(Vector2Integer a, Vector2Integer b)
            => new Vector2Integer(a.X + b.X, a.Y + b.Y);

        public static Vector2Integer operator -(Vector2Integer a, Vector2Integer b)
            => new Vector2Integer(a.X - b.X, a.Y - b.Y);

        public static Vector2Integer operator *(Vector2Integer a, int factor)
            => new Vector2Integer(a.X * factor, a.Y * factor);
    }
}