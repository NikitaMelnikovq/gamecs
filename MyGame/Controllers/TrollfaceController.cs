using Microsoft.Xna.Framework;
using MyGame.Models;
using System;
using Microsoft.Xna.Framework.Media;

namespace MyGame.Controllers
{
    public class TrollfaceController
    {
        private readonly TrollfaceModel trollfaceModel;
        private Vector2 playerPosition;
        private readonly float escapeSpeed;
        private readonly float teleportCooldown;
        private readonly float panicRadius;
        private float timeNearPlayerAndBorder;
        private float timeSinceLastTeleport;
        private float timeSinceLastDirectionChange;
        private readonly Random random = new Random();
        private Game1 game;

        public TrollfaceController(TrollfaceModel model, Vector2 initialPlayerPosition, float escapeSpeed, float maxSpeed, float teleportCooldown, Game1 game, float panicRadius = 50f)
        {
            trollfaceModel = model;
            playerPosition = initialPlayerPosition;
            this.escapeSpeed = escapeSpeed;
            this.teleportCooldown = teleportCooldown;
            this.panicRadius = panicRadius;
            timeNearPlayerAndBorder = 0f;
            timeSinceLastTeleport = 0f;
            this.game = game;
            timeSinceLastDirectionChange = 0f;
        }

        public void UpdatePlayerPosition(Vector2 newPlayerPosition)
        {
            playerPosition = newPlayerPosition;
        }

        public void Update(GameTime gameTime)
        {
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastTeleport += elapsed;
            timeSinceLastDirectionChange += elapsed;

            // Change direction randomly every few seconds
            if (timeSinceLastDirectionChange >= 2f)
            {
                ChangeRandomDirection();
                timeSinceLastDirectionChange = 0f;
            }

            // Calculate distance to player
            var toPlayer = playerPosition - trollfaceModel.Position;
            var distanceToPlayer = toPlayer.Length();

            // Check if troll is near player and boundary
            var isNearPlayer = distanceToPlayer < panicRadius;
            var isNearBoundary = IsNearBoundary(trollfaceModel.Position);

            if (isNearPlayer && isNearBoundary)
            {
                timeNearPlayerAndBorder += elapsed;
                if (timeNearPlayerAndBorder >= 3f && timeSinceLastTeleport >= teleportCooldown)
                {
                    TeleportToCenter();
                    timeSinceLastTeleport = 0f;
                    timeNearPlayerAndBorder = 0f;
                    game.PlayTeleportSound();
                }
            }
            else
            {
                timeNearPlayerAndBorder = 0f;
            }

            // Escape from player if near
            if (isNearPlayer)
            {
                Vector2 escapeDirection = trollfaceModel.Position - playerPosition;
                escapeDirection.Normalize();
                trollfaceModel.Velocity = escapeDirection * escapeSpeed;
            }

            // Update model position
            trollfaceModel.Update(gameTime);
        }

        private void ChangeRandomDirection()
        {
            var speed = trollfaceModel.MaxSpeed * (float)(random.NextDouble() * 0.5 + 0.5); // Random speed between 0.5x and 1.5x max speed
            var angle = (float)(random.NextDouble() * Math.PI * 2); // Random angle

            trollfaceModel.Velocity = new Vector2(
                (float)Math.Cos(angle),
                (float)Math.Sin(angle)
            ) * speed;
        }

        private void TeleportToCenter()
        {
            var center = new Vector2(GameConstants.ScreenWidth / 2, GameConstants.ScreenHeight / 2);
            trollfaceModel.Position = center;
        }

        private bool IsNearBoundary(Vector2 position)
        {
            var boundaryThreshold = 25f; // расстояние от границы для рассмотрения как "рядом"
            return position.X < boundaryThreshold ||
                   position.X > GameConstants.ScreenWidth - boundaryThreshold ||
                   position.Y < boundaryThreshold ||
                   position.Y > GameConstants.ScreenHeight - boundaryThreshold;
        }
    }
}
