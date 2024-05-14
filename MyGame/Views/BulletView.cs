using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Models;
using System.Collections.Generic;

namespace MyGame.Views
{
    public class BulletView
    {
        private SpriteBatch spriteBatch;
        private Texture2D bulletTexture; 
        public BulletView(SpriteBatch spriteBatch, Texture2D bulletTexture)
        {
            this.spriteBatch = spriteBatch;
            this.bulletTexture = bulletTexture;
        }

        public void Draw(List<Bullet> bullets)
        {
            foreach (Bullet bullet in bullets)
            {
                spriteBatch.Draw(bulletTexture, bullet.Position, null, Color.White, bullet.Rotation, new Vector2(bulletTexture.Width / 2, bulletTexture.Height / 2), 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}