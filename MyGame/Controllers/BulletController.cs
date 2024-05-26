using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MyGame.Models;

namespace MyGame.Controllers
{
    public class BulletController
    {
        private readonly List<Bullet> bullets;
        private readonly Texture2D bulletTexture;
        private readonly Rectangle bounds;
        private readonly float bulletSpeed;

        public BulletController(Texture2D bulletTexture, Rectangle bounds, float bulletSpeed)
        {
            this.bulletTexture = bulletTexture;
            this.bounds = bounds;
            this.bulletSpeed = bulletSpeed;
            bullets = new List<Bullet>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var bullet in bullets)
            {
                bullet.Update(gameTime);
            }

            // Удаление неактивных снарядов
            bullets.RemoveAll(b => !b.IsActive);
        }

        public void FireBullet(Vector2 position, float rotation)
        {
            var bullet = new Bullet(position, rotation, bulletSpeed, bounds);
            bullets.Add(bullet);
        }

        public List<Bullet> Bullets => bullets;
    }
}