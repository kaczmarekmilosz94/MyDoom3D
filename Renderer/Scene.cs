using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderEngine
{
    public class Scene
    {
        public Wall[] Walls;
        public Color Ceiling { get; set; }
        public Color Floor { get; set; }

        public Sprite[] Sprites { get; set; }

        public Sprite PlayerSprite { get; set; }

        public Scene(Wall[] walls, Sprite[] sprites, Sprite player)
        {
            Walls = walls;
            Sprites = sprites;
            PlayerSprite = player;
        }
    }
}
