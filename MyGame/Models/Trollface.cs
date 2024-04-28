using Microsoft.Xna.Framework;

namespace MyGame.Models
{
    public class TrollfaceModel
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float MaxSpeed { get; }
        public Rectangle Bounds { get; }

        public TrollfaceModel(Vector2 position, Vector2 velocity, float maxSpeed, Rectangle bounds)
        {
            Position = position;
            Velocity = velocity;
            MaxSpeed = maxSpeed;
            Bounds = bounds;
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Velocity.Length() > MaxSpeed)
            {
                Velocity.Normalize();
                Velocity *= MaxSpeed;
            }
        }
    }
}