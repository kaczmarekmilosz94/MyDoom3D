using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RenderEngine;

namespace MyDoom
{
    public partial class Form1 : Form
    {
        Renderer renderer;
        Player player;
        Scene scene;
        Graphics target;

        Camera camera = new Camera(new Vector2(50, 180), new CameraSettings()
        {
            RaysCount = 200,
            FOV = 45
        });

        Mob[] mobs = new Mob[2];

        public Form1()
        {
            InitializeComponent();
        }

        // Start
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateScene();
            renderer = new Renderer(camera);
        }

        // Update      
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            target = e.Graphics;
            target.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            renderer.Render(scene, target);

            DrawMap(
               walls: true, mobs: true, rays: true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
           
            foreach (var mob in mobs)
            {
                mob.FocusOnTarget(player);
            }
        }


        private void DrawMap(bool walls, bool mobs, bool rays) 
        {
            var camera = player.Camera;

            if (walls)
            {
                foreach (var wall in scene.Walls)
                {
                    target.DrawLine(new Pen(new SolidBrush(Color.Black)), (float)wall.StartPoint.X, (float)wall.StartPoint.Y, (float)wall.EndPoint.X, (float)wall.EndPoint.Y);
                }
            }
            if (mobs)
            {
                foreach (var sprite in scene.Sprites)
                {
                    target.DrawLine(new Pen(new SolidBrush(Color.HotPink)), (float)sprite.StartPoint.X, (float)sprite.StartPoint.Y, (float)sprite.EndPoint.X, (float)sprite.EndPoint.Y);
                } 
            }
            if (rays)
            {
                foreach (var ep in camera.CastRays(scene.Walls, out _))
                {
                    if (ep != null) target.DrawLine(new Pen(new SolidBrush(Color.Blue)), (float)camera.Position.X, (float)camera.Position.Y, (float)ep.X, (float)ep.Y);
                }
            }
        }
        private void CreateScene() 
        {
            Wall[] walls = new Wall[4];

            walls[0] = new Wall()
            {
                StartPoint = new Vector2(200, 320),
                EndPoint = new Vector2(50, 20),
                Color = Color.DarkBlue

            };
            walls[1] = new Wall()
            {
                StartPoint = new Vector2(10, 10),
                EndPoint = new Vector2(50, 220),
                Color = Color.IndianRed
            };
            walls[2] = new Wall()
            {
                StartPoint = new Vector2(200, 120),
                EndPoint = new Vector2(50, 220),
                Color = Color.Gold
            };
            walls[3] = new Wall()
            {
                StartPoint = new Vector2(300, 320),
                EndPoint = new Vector2(250, 220),
                Color = Color.GreenYellow
            };



            mobs[0] = new Mob() { Position = new Vector2(100, 100), Speed = 0.2f, AggroDistance = 100, AtackDistance = 5};
            mobs[1] = new Mob() { Position = new Vector2(200, 200), Speed = 0.1f, AggroDistance = 100, AtackDistance = 5};
            mobs[0].Sprite = new Sprite("test.png")
            {
                StartPoint = new Vector2(100, 100),
                EndPoint = new Vector2(101, 100),
                Position = new Vector2(100, 100)
            };
            mobs[1].Sprite = new Sprite("test.png")
            {
                StartPoint = new Vector2(200, 200),
                EndPoint = new Vector2(201, 200),
                Position = new Vector2(200, 200)
            };

            var sprites = new Sprite[] { mobs[0].Sprite, mobs[1].Sprite };


            player = new Player(camera)
            {
                Position = new Vector2(50, 180),
                Sprite = new Sprite("player.png")
            };


            scene = new Scene(walls, sprites, player.Sprite);
            scene.Ceiling = Color.BlanchedAlmond;
            scene.Floor = Color.DarkGray;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.MoveLocal(new Vector2(5, 0));
                    break;
                case Keys.S:
                    player.MoveLocal(new Vector2(-5, 0));
                    break;
                case Keys.A:
                    player.Rotate(-5);
                    break;
                case Keys.D:
                    player.Rotate(5);
                    break;
                default:
                    break;
            }
        }      
    }
}
