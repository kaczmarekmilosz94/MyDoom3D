using RenderEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine
{
    public class Sprite : ISceneObject
    {
        public Vector2 Position { get; set; }
        public Vector2 StartPoint { get; set; }
        public Vector2 EndPoint { get; set; }
        public string ImagePath { get; set; }

        public Sprite(string imagePath)
        {
            ImagePath = imagePath;
        }

        public void LookAt(Vector2 target) 
        {
            var vect = Position - target;

            vect.Normalize();

            vect = new Vector2(-vect.Y, vect.X);

            StartPoint = Position - vect;
            EndPoint = Position + vect;
        }
    }
}
