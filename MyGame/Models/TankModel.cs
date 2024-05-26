using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Interfaces;

namespace MyGame.Models
{
    public class TankModel : IBoundable
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Speed { get; set; }
        public bool IsDestroyed { get; set; }
        public Rectangle Bounds { get; set; }
        public Texture2D Texture { get; set; }

        public TankModel(Vector2 position, float rotation, float speed, Rectangle bounds, Texture2D texture)
        {
            Position = position;
            Rotation = rotation;
            Speed = speed;
            IsDestroyed = false;
            Bounds = bounds;
            Texture = texture;
        }

        public void CheckBounds(Rectangle bounds)
        {
            Vector2 newPosition = Position;
            newPosition.X = MathHelper.Clamp(newPosition.X, bounds.Left + Texture.Width / 2, bounds.Right - Texture.Width / 2);
            newPosition.Y = MathHelper.Clamp(newPosition.Y, bounds.Top + Texture.Height / 2, bounds.Bottom - Texture.Height / 2);
            Position = newPosition;
        }
    }
}