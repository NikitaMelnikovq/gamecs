using Microsoft.Xna.Framework;

namespace MyGame.Models
{
    public class TankModel
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Speed { get; set; }
        public bool IsDestroyed { get; set; }

        public Rectangle Bounds { get; set; }

        public TankModel(Vector2 position, float rotation, float speed, Rectangle bounds)
        {
            Position = position;
            Rotation = rotation;
            Speed = speed;
            IsDestroyed = false;
            Bounds = bounds;
        }
    }
}