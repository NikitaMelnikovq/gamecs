using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Models;

namespace MyGame.Views
{
    public class TrollfaceView
    {
        private Texture2D texture;
        private Rectangle sourceRectangle;
        private Vector2 origin;

        public TrollfaceView(Texture2D texture)
        {
            this.texture = texture;
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        // Метод для отрисовки Trollface
        public void Draw(SpriteBatch spriteBatch, TrollfaceModel trollfaceModel)
        {
            spriteBatch.Draw(texture, trollfaceModel.Position, sourceRectangle, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}