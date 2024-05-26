using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Models;

namespace MyGame.Views
{
    public class TankView
    {
        private readonly Texture2D texture;
        private Rectangle sourceRectangle;
        private Vector2 origin;

        public TankView(Texture2D texture)
        {
            this.texture = texture;
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch, TankModel tankModel)
        {
            spriteBatch.Draw(texture, tankModel.Position, sourceRectangle, Color.White, tankModel.Rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}