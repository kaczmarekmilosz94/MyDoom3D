using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine
{
    public class Wall : ISceneObject
    {
        public Vector2 StartPoint { get; set; }
        public Vector2 EndPoint { get; set; }
        public Color Color { get; set; }
    }
}
