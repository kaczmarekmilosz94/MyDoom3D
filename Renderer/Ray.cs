using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RenderEngine
{
    internal class Ray
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }


        public Vector2 FindIntersection(ISceneObject obj)
        {
            double x1 = obj.StartPoint.X;
            double y1 = obj.StartPoint.Y;
            double x2 = obj.EndPoint.X;
            double y2 = obj.EndPoint.Y;

            double x3 = Position.X;
            double y3 = Position.Y;
            double x4 = Direction.X;
            double y4 = Direction.Y;

            double den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (den == 0) 
            {
                return null; 
            }

            double t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            double u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;

            if (t > 0 && t < 1 && u > 0) 
            {
                double x = x1 + t * (x2 - x1);
                double y = y1 + t * (y2 - y1);

                return new Vector2(x,y);
            }
            else 
            {
                return null; 
            }
        }
        public Vector2 FindIntersection(ISceneObject[] objects, out int objectId) 
        {
            Vector2 vector = null;
            double minDistance = float.MaxValue;

            objectId = -1;
            int id = 0;

            foreach (var obj in objects)
            {
                var interPoint = FindIntersection(obj);

                if (interPoint != null)
                {
                    double distance = interPoint.DistanceSquared(this.Position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;

                        vector = interPoint;
                        objectId = id;
                    }
                }
                id++;
            }
            return vector;
        }
    }
}
