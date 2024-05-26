using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyGame.Models;

namespace MyGame.Controllers
{
    public class TankController
    {
        private readonly TankModel tankModel;
        private readonly TrollfaceModel trollfaceModel;

        public TankController(TankModel tankModel, TrollfaceModel trollfaceModel)
        {
            this.tankModel = tankModel;
            this.trollfaceModel = trollfaceModel;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var direction = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                direction.Y = -1;
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                direction.Y = 1;

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                direction.X = -1;
            else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                direction.X = 1;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();

                var newPosition = tankModel.Position + direction * tankModel.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                var tankRect = new Rectangle(newPosition.ToPoint(), new Point(tankModel.Texture.Width, tankModel.Texture.Height));
                var trollRect = new Rectangle(trollfaceModel.Position.ToPoint(), new Point(trollfaceModel.Texture.Width, trollfaceModel.Texture.Height));

                if (tankRect.Intersects(trollRect))
                {
                    // Обработка столкновения (например, остановка танка или отскок)
                }
                else
                {
                    tankModel.Position = newPosition;
                    tankModel.CheckBounds(tankModel.Bounds);
                }
            }

            if (keyboardState.IsKeyDown(Keys.Q))
                tankModel.Rotation -= 0.03f;

            if (keyboardState.IsKeyDown(Keys.E))
                tankModel.Rotation += 0.03f;
        }
    }
}
