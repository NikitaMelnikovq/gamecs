using Microsoft.Xna.Framework;
using System;

namespace YourGameProject.Models
{
    public class Bullet
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Speed { get; set; }
        public Rectangle Bounds { get; set; }
        public bool IsActive { get; set; }

        public Bullet(Vector2 position, float rotation, float speed, Rectangle bounds)
        {
            Position = position;
            Rotation = rotation;
            Speed = speed;
            Bounds = bounds;
            IsActive = true;
        }

        public void Move()
        {
            Position += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * Speed;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

    }
}