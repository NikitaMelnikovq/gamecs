using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Models
{
    public class TrollfaceModel
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float MaxSpeed { get; }
        public Rectangle Bounds { get; }
        public Texture2D Texture { get; } // Добавлено свойство Texture

        public TrollfaceModel(Vector2 position, Vector2 velocity, float maxSpeed, Rectangle bounds, Texture2D texture)
        {
            Position = position;
            Velocity = velocity;
            MaxSpeed = maxSpeed;
            Bounds = bounds;
            Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Velocity.Length() > MaxSpeed)
            {
                Velocity.Normalize();
                Velocity *= MaxSpeed;
            }

            CheckBounds();
        }

        public void CheckBounds()
        {
            Position = new Vector2(
                MathHelper.Clamp(Position.X, Bounds.Left, Bounds.Right - Texture.Width),
                MathHelper.Clamp(Position.Y, Bounds.Top, Bounds.Bottom - Texture.Height)
            );
        }
    }
}
