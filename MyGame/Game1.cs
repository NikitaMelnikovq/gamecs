using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Models;
using MyGame.Views;
using MyGame.Controllers;

namespace MyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private Texture2D tankTexture;
        private TankModel tankModel;
        private TankView tankView;
        private TankController tankController;

        private Texture2D trollfaceTexture;
        private TrollfaceModel trollfaceModel;
        private TrollfaceView trollfaceView;
        private TrollfaceController trollfaceController;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Загрузка текстур танка и тролля
            tankTexture = Content.Load<Texture2D>("tankTexture");
            trollfaceTexture = Content.Load<Texture2D>("trollface");

            // Инициализация моделей, представлений и контроллеров для танка
            tankModel = new TankModel(new Vector2(100, 100), 0f, 2f, new Rectangle(0, 0, tankTexture.Width, tankTexture.Height));
            tankView = new TankView(tankTexture);
            tankController = new TankController(tankModel);

            // Инициализация моделей, представлений и контроллеров для тролля
            trollfaceModel = new TrollfaceModel(new Vector2(100, 100), Vector2.Zero, 100f, new Rectangle(0, 0, 800, 600));
            trollfaceView = new TrollfaceView(trollfaceTexture);
            trollfaceController = new TrollfaceController(trollfaceModel);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Обновление состояния танка и тролля
            tankController.Update(gameTime);
            trollfaceController.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Отрисовка танка и тролля
            tankView.Draw(spriteBatch, tankModel);
            trollfaceView.Draw(spriteBatch, trollfaceModel);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}