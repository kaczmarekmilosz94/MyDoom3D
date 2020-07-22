using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine
{
    public class Camera
    {
        public CameraSettings Settings { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }

        private double _localRotation; 


        public Camera(Vector2 position, CameraSettings settings)
        {
            Position = position;
            Direction = new Vector2(1, 0);
            Settings = settings;
        }

        public Vector2[] CastRays(ISceneObject[] objects, out int[] objectIds)
        {
            var vectors = new Vector2[Settings.RaysCount];

            objectIds = new int[Settings.RaysCount];

            for (int i = 0; i < Settings.RaysCount; i++)
            {
                var ray = new Ray()
                {
                    Position = this.Position,
                    Direction = this.Direction + Position
                };
                ray.Direction.RotateAroundPoint(Position, (i - Settings.RaysCount /2) * (Settings.FOV/Settings.RaysCount));

                vectors[i] = ray.FindIntersection(objects, out objectIds[i]);
            }
            return vectors;
        }

        public void Rotate(double degrees) 
        {
            _localRotation += degrees;
            Direction.RotateAroundPoint(new Vector2(0,0), degrees);
        }
        public void MoveLocal(Vector2 vector) 
        {
            vector.RotateAroundPoint(new Vector2(0, 0), _localRotation);
            Position += vector;
        }
    }
}
