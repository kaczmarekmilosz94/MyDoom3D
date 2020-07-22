using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine
{
    public class Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceSquared(Vector2 target)
        {
            double x = this.X - target.X;
            double y = this.Y - target.Y;

            return (x * x + y * y);
        }     
        public void RotateAroundPoint(Vector2 point, double degrees)
        {
            double radians = degrees * (Math.PI / 180);

            if (degrees != 0)
            {
                double sin = Math.Sin(radians);
                double cos = Math.Cos(radians);

                X -= point.X;
                Y -= point.Y;

                double x = X * cos - Y * sin;
                double y = X * sin + Y * cos;

                X = x + point.X;
                Y = y + point.Y;
            }
        }

        public void Normalize()
        {
            double len = Math.Sqrt(X*X + Y*Y);

            X /= len;
            Y /= len;
        }


        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.X * b, a.Y * b);
    }
}
