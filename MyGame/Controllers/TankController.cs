using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyGame.Models;
using System;

namespace MyGame.Controllers
{
   public class TankController
    {
        private readonly TankModel tankModel;
        private readonly TrollfaceModel trollfaceModel;
        private readonly BulletController bulletController;
        private double lastFireTime;

        public TankController(TankModel tankModel, TrollfaceModel trollfaceModel, BulletController bulletController)
        {
            this.tankModel = tankModel;
            this.trollfaceModel = trollfaceModel;
            this.bulletController = bulletController;
            lastFireTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (tankModel.IsDestroyed) return;

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
                    tankModel.IsDestroyed = true; 
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

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                FireBullet(gameTime);
            }
        }

        private void FireBullet(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastFireTime > 0.5)
            {
                var bulletPosition = tankModel.Position + new Vector2((float)Math.Cos(tankModel.Rotation), (float)Math.Sin(tankModel.Rotation)) * (tankModel.Texture.Height / 2);
                bulletController.FireBullet(bulletPosition, tankModel.Rotation);
                lastFireTime = gameTime.TotalGameTime.TotalSeconds;
            }
        }
    }

}
