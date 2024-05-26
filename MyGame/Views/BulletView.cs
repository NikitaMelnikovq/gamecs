using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MyGame.Models;

namespace MyGame.Views
{
    public class BulletView
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D texture;

        public BulletView(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }

        public void Draw(List<Bullet> bullets)
        {
            foreach (var bullet in bullets)
            {
                if (bullet.IsActive)
                {
                    spriteBatch.Draw(texture, bullet.Position, null, Color.White, bullet.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}