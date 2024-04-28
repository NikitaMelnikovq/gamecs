using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class MenuButton
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _rectangle;

        public MenuButton(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            _rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Rectangle Rectangle
        {
            get { return _rectangle; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}