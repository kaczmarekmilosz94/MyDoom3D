using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine
{
    public interface ISceneObject
    {
        Vector2 StartPoint { get; set; }
        Vector2 EndPoint { get; set; }
    }
}
