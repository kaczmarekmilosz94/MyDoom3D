using RenderEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoom
{
    class Player : IKillable
    {
        public Vector2 Position { get; set; }
        public Sprite Sprite { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }


        public readonly Camera Camera;

        public Player(Camera camera)
        {
            Camera = camera;
        }

        public void Rotate(double degrees)
        {
            Camera.Rotate(degrees);
        }
        public void MoveLocal(Vector2 vector)
        {
            Camera.MoveLocal(vector);
            Position = Camera.Position;
        }
    }
}
