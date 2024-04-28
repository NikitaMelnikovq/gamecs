using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyGame.Models;

namespace MyGame.Controllers
{
    public class TankController
    {
        private readonly TankModel tankModel;

        public TankController(TankModel tankModel)
        {
            this.tankModel = tankModel;
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

                var newPosition = tankModel.Position + direction * tankModel.Speed;
                newPosition.X = MathHelper.Clamp(newPosition.X, 0, 800 - tankModel.Bounds.Width);
                newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, 600 - tankModel.Bounds.Height);

                tankModel.Position = newPosition;
            }

            if (keyboardState.IsKeyDown(Keys.Q))
                tankModel.Rotation -= 0.03f;

            if (keyboardState.IsKeyDown(Keys.E))
                tankModel.Rotation += 0.03f;
        }
    }
}