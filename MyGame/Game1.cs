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

        private BulletController bulletController;
        private BulletView bulletView;
        private Texture2D bulletTexture;
        private Bullet bulletModel;

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
            var tankSpeed = 100f; // Скорость танка

            // Загрузка текстур 
            tankTexture = Content.Load<Texture2D>("tankTexture");
            trollfaceTexture = Content.Load<Texture2D>("trollface");
            bulletTexture = Content.Load<Texture2D>("BulletTexture");

            // Инициализация моделей
            tankModel = new TankModel(new Vector2(100, 100), 0f, tankSpeed, new Rectangle(0, 0, 800, 600), tankTexture);
            trollfaceModel = new TrollfaceModel(new Vector2(300, 300), Vector2.Zero, 100f, new Rectangle(0, 0, 800, 600), trollfaceTexture); // Передаем текстуру тролля
            bulletModel = new Bullet(new Vector2(0, 0), 0f, 5f, new Rectangle(0, 0, bulletTexture.Width, bulletTexture.Height));

            // Инициализация представлений
            trollfaceView = new TrollfaceView(trollfaceTexture);
            tankView = new TankView(tankTexture);
            bulletView = new BulletView(spriteBatch, bulletTexture);

            // Инициализация контроллеров 
            trollfaceController = new TrollfaceController(trollfaceModel, tankModel.Position, tankSpeed * 2.0f, tankSpeed * 27f, 3f);
            tankController = new TankController(tankModel, trollfaceModel);
            bulletController = new BulletController(bulletTexture, tankModel.Bounds, 5f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Обновление состояния
            tankController.Update(gameTime);
            trollfaceController.Update(gameTime);
            bulletController.Update(gameTime);
            trollfaceController.UpdatePlayerPosition(tankModel.Position);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Отрисовка
            tankView.Draw(spriteBatch, tankModel);
            trollfaceView.Draw(spriteBatch, trollfaceModel);
            bulletView.Draw(bulletController.Bullets);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
