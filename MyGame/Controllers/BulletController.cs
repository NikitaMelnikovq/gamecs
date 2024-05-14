using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MyGame.Models;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Controllers
{
    public class BulletController
    {
        private List<Bullet> bullets;
        private Texture2D bulletTexture;
        private Rectangle tankBounds; 
        private float bulletSpeed; 

        public BulletController(Texture2D bulletTexture, Rectangle tankBounds, float bulletSpeed)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
            this.tankBounds = tankBounds;
            this.bulletSpeed = bulletSpeed;
        }

        public void Update(GameTime gameTime)
        {
            // Обновление каждого снаряда
            foreach (Bullet bullet in bullets)
            {
                bullet.Move();
                if (!tankBounds.Contains(bullet.Bounds))
                {
                    bullet.Deactivate(); 
                }
            }
            bullets.RemoveAll(bullet => !bullet.IsActive);
        }

        public void Shoot(Vector2 position, float rotation)
        {
            Bullet newBullet = new Bullet(position, rotation, bulletSpeed, new Rectangle((int)position.X, (int)position.Y, bulletTexture.Width, bulletTexture.Height));
            bullets.Add(newBullet);
        }

        public void HandleInput(MouseState currentMouseState, MouseState previousMouseState, Vector2 tankPosition, float tankRotation)
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                Shoot(tankPosition, tankRotation);
            }
        }

        public List<Bullet> Bullets
        {
            get { return bullets; }
        }
    }
}