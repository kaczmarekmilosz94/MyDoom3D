using RenderEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoom
{
    class Mob : IKillable
    {
        public Vector2 Position { get; set ; }
        public Sprite Sprite { get; set; }

        public int Health { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }

        public double AggroDistance { get; set; }
        public double AtackDistance { get; set; }

        public void Transform(Vector2 vector) 
        {
            Position += vector;
            Sprite.Position += vector;
        }

        public void MoveTowards(Vector2 target) 
        {
            var dir = Position - target;
            dir.Normalize();
            Transform(dir * -Speed);
        }

        public void FocusOnTarget(IKillable target) 
        {
            Sprite.LookAt(target.Position);

            var disSquared = Position.DistanceSquared(target.Position);

            if (disSquared < AggroDistance * AggroDistance) 
            {
                if (disSquared < AtackDistance * AtackDistance)
                    target.Health -= Damage;

                else
                    MoveTowards(target.Position);
            }
        }
    }
}
