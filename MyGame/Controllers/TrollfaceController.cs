using Microsoft.Xna.Framework;
using MyGame.Models;
using System;
using Microsoft.Xna.Framework.Media;

namespace MyGame.Controllers
{
public class TrollfaceController
    {
        private readonly TrollfaceModel trollfaceModel;
        private readonly TankModel tankModel;
        private float chaseSpeed;
        private float fleeSpeed;
        private float teleportCooldown;
        private double lastTeleportTime;
        private Game1 game;

        public TrollfaceController(TrollfaceModel trollfaceModel, TankModel tankModel, float chaseSpeed, float fleeSpeed, float teleportCooldown, Game1 game)
        {
            this.trollfaceModel = trollfaceModel;
            this.tankModel = tankModel;
            this.chaseSpeed = chaseSpeed;
            this.fleeSpeed = fleeSpeed;
            this.teleportCooldown = teleportCooldown;
            this.game = game;
            lastTeleportTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            float distance = Vector2.Distance(trollfaceModel.Position, tankModel.Position);
            if (distance < 100f && gameTime.TotalGameTime.TotalSeconds - lastTeleportTime > teleportCooldown)
            {
                Teleport();
                game.TankModel.Health -= 10; // Уменьшение здоровья танка при телепортации тролля
                lastTeleportTime = gameTime.TotalGameTime.TotalSeconds;
            }
            else
            {
                Vector2 direction = Vector2.Zero;

                if (distance < 200f)
                {
                    direction = trollfaceModel.Position - tankModel.Position;
                    direction.Normalize();
                    trollfaceModel.Position += direction * fleeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (distance > 300f)
                {
                    direction = tankModel.Position - trollfaceModel.Position;
                    direction.Normalize();
                    trollfaceModel.Position += direction * chaseSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                trollfaceModel.CheckBounds(); // вызов метода без параметров
            }
        }

        public void UpdatePlayerPosition(Vector2 newPosition)
        {
            // Обновление позиции игрока
        }

        private void Teleport()
        {
            var random = new Random();
            var newPosition = new Vector2(random.Next(0, 800), random.Next(0, 600));
            trollfaceModel.Position = newPosition;
        }
    }

}
