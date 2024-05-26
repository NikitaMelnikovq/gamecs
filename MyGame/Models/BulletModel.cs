using System;
using Microsoft.Xna.Framework;

namespace MyGame.Models
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

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Деактивируем снаряд, если он выходит за границы экрана
                if (Position.X < 0 || Position.X > Bounds.Width || Position.Y < 0 || Position.Y > Bounds.Height)
                {
                    IsActive = false;
                }
            }
        }
    }
}
