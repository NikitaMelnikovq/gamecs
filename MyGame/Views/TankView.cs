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
            // Вычисляем позицию танка, чтобы он не выходил за границы экрана
            var clampedPosition = new Vector2(
                MathHelper.Clamp(tankModel.Position.X, 0, GameConstants.ScreenWidth - texture.Width),
                MathHelper.Clamp(tankModel.Position.Y, 0, GameConstants.ScreenHeight - texture.Height)
            );

            spriteBatch.Draw(texture, clampedPosition, sourceRectangle, Color.White, tankModel.Rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
