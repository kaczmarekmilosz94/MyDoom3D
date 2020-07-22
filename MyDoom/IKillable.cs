using RenderEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoom
{
    internal interface IKillable
    {
        Vector2 Position { get; set; }
        int Health { get; set; }
        int Damage { get; set; }
        float Speed { get; set; }
    }
}
