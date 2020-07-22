using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RenderEngine
{
    public class Renderer
    {
        public Camera Camera { get; set; }
        // window/game settings

        public Renderer(Camera camera)
        {
            Camera = camera;
        }

        public void Render(Scene scene, Graphics target) 
        {
            if (scene == null) return;

            double width = 800 / Camera.Settings.RaysCount;
            double maxH = 600;

            double maxDistance = 1000;
            double minDistance = 0.1f;

            var crossPointsWalls = Camera.CastRays(scene.Walls, out int[] wallIds);
            var crossPointsSprites = Camera.CastRays(scene.Sprites, out int[] spriteIds);


            target.FillRectangle(new SolidBrush(scene.Ceiling), 0, 0, 800f, 300f);
            target.FillRectangle(new SolidBrush(scene.Floor), 0, 300, 800f, 300f);

            for (int i = 0; i < Camera.Settings.RaysCount; i++)
            {
                if (crossPointsWalls[i] == null) continue;

                double posX = width * i;
                double posY = 0;
                double height;

                double dist = Math.Sqrt(crossPointsWalls[i].DistanceSquared(Camera.Position));

                if (dist <= maxDistance && dist >= minDistance) 
                {
                    double size = (maxDistance / dist) * 0.005f;
                    height = size * maxH;
                    var rect = new RectangleF((float)posX, (float)(posY + (maxH / 2) - height), (float)width, (float)(2 * height));
                    var color = scene.Walls[wallIds[i]].Color;

                    if (size > 1) size = 1;

                    color = Color.FromArgb(
                        255,
                        (int)(color.R - (color.R * size)),
                        (int)(color.G - (color.G * size)),
                        (int)(color.B - (color.B * size)));

                    target.FillRectangle(new SolidBrush(color), rect);
                }               
            }

            List<int> renderedSprites = new List<int>();

            for (int i = 0; i < Camera.Settings.RaysCount; i++)
            {
                if (renderedSprites.Contains(spriteIds[i])) continue;
                else renderedSprites.Add(spriteIds[i]);

                if (crossPointsSprites[i] == null) continue;
                if (crossPointsWalls[i] != null &&
                    crossPointsSprites[i].DistanceSquared(Camera.Position) > crossPointsWalls[i].DistanceSquared(Camera.Position)) continue;


                double dist = Math.Sqrt(crossPointsSprites[i].DistanceSquared(Camera.Position));

                if (dist <= maxDistance && dist >= minDistance)
                {
                    using (Image image = Image.FromFile(scene.Sprites[spriteIds[i]].ImagePath))
                    {
                        double posX = width * i;
                        double posY = 0;

                        double size = (maxDistance / dist) * 0.005f;
                        double height = size * image.Height;

                        var rect = new Rectangle((int)posX, (int)(posY + (maxH / 2) - height), (int)(image.Width * size*2), (int)(2 * height));

                        target.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                    }
                }
            }

            using (Image image = Image.FromFile(scene.PlayerSprite.ImagePath))
            {
                target.DrawImage(image, 500-image.Width, 515-image.Height);
            }
        }
    }
}
