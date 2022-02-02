using System;

public class Point : Godot.Object, IEquatable<Point>  {
    public readonly int X;
    public readonly int Y;

    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
    public static bool operator >(Point a, Point b) => a.X + a.Y > b.X + b.Y;
    public static bool operator <(Point a, Point b) => a.X + a.Y < b.X + b.Y;

    public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Point a, Point b) => a.X != b.X || a.Y != b.Y;

    public bool Equals(Point other)
    {
        if (ReferenceEquals(null, other))
                return false;
        if (ReferenceEquals(this, other))
                return true;
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

    public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Point)obj);
        }
} 