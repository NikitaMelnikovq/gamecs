using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Models;

namespace MyGame.Views
{
    public class TrollfaceView
{
    private Texture2D texture;
    private SpriteFont font;

    public TrollfaceView(Texture2D texture, SpriteFont font)
    {
        this.texture = texture;
        this.font = font;
    }

    public void Draw(SpriteBatch spriteBatch, TrollfaceModel model)
    {
        spriteBatch.Draw(texture, model.Position, Color.White);

        // Отображение здоровья над головой тролля
        var healthText = model.Health.ToString();
        var healthPosition = new Vector2(model.Position.X + texture.Width / 2, model.Position.Y - 20);
        spriteBatch.DrawString(font, healthText, healthPosition, Color.Red);
    }
}

}