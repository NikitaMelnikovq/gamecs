using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Button
{
    private Texture2D texture;
    private SpriteFont font;
    private bool isHovering;
    private MouseState currentMouse;
    private MouseState previousMouse;

    public event EventHandler Click;
    public bool Clicked { get; private set; }
    public Vector2 Position { get; set; }
    public string Text { get; set; }

    public Button(Texture2D texture, SpriteFont font)
    {
        this.texture = texture;
        this.font = font;
    }

    public void Update(GameTime gameTime)
    {
        previousMouse = currentMouse;
        currentMouse = Mouse.GetState();

        var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

        isHovering = false;

        if (mouseRectangle.Intersects(new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height)))
        {
            isHovering = true;

            if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this, new EventArgs());
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var color = Color.White;

        if (isHovering)
        {
            color = Color.Gray;
        }

        spriteBatch.Draw(texture, Position, color);

        if (!string.IsNullOrEmpty(Text))
        {
            var x = (Position.X + (texture.Width / 2)) - (font.MeasureString(Text).X / 2);
            var y = (Position.Y + (texture.Height / 2)) - (font.MeasureString(Text).Y / 2);

            spriteBatch.DrawString(font, Text, new Vector2(x, y), Color.Black);
        }
    }
}
