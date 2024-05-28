using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using MyGame.Controllers;
using MyGame.Models;
using MyGame.Views;

public enum GameState
{
    MainMenu,
    Playing,
    Settings,
    GameOver,
}

namespace MyGame
{
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch spriteBatch;
    private Texture2D tankTexture;
    private TankView tankView;
    private TankController tankController;
    private Texture2D trollfaceTexture;
    private TrollfaceModel trollfaceModel;
    private TrollfaceView trollfaceView;
    private TrollfaceController trollfaceController;
    private BulletController bulletController;
    private BulletView bulletView;
    private Texture2D bulletTexture;
    private SpriteFont font;
    private Texture2D buttonTexture;
    private List<Button> buttons;
    private Song teleportSound;
    private GameState currentGameState;
    private float volumeLevel;
    private KeyboardState previousKeyboardState;
    private double gameOverTime;

    public TankModel TankModel { get; private set; }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        currentGameState = GameState.MainMenu;
        volumeLevel = 0.5f;
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
        teleportSound = Content.Load<Song>("Fart_Meme_Sound");
        font = Content.Load<SpriteFont>("Fonts/Roboto");
        buttonTexture = Content.Load<Texture2D>("ButtonTexture");

        // Инициализация моделей
        TankModel = new TankModel(new Vector2(100, 100), 0f, tankSpeed, new Rectangle(0, 0, 800, 600), tankTexture);
        trollfaceModel = new TrollfaceModel(new Vector2(300, 300), Vector2.Zero, 100f, new Rectangle(0, 0, 800, 600), trollfaceTexture); // Передаем текстуру тролля

        // Инициализация представлений
        trollfaceView = new TrollfaceView(trollfaceTexture, font);
        tankView = new TankView(tankTexture);
        bulletView = new BulletView(spriteBatch, bulletTexture);

        // Инициализация контроллеров 
        bulletController = new BulletController(bulletTexture, new Rectangle(0, 0, 800, 600), 300f); // Убедитесь, что скорость пули достаточно высока
        tankController = new TankController(TankModel, trollfaceModel, bulletController);
        trollfaceController = new TrollfaceController(trollfaceModel, TankModel, tankSpeed * 2.0f, tankSpeed * 2.7f, 3f, this);

        // Инициализация кнопок
        buttons = new List<Button>
        {
            new Button(buttonTexture, font) { Text = "Start Game", Position = new Vector2(250, 200) },
            new Button(buttonTexture, font) { Text = "Settings", Position = new Vector2(250, 300) },
            new Button(buttonTexture, font) { Text = "Exit", Position = new Vector2(250, 400) }
        };

        buttons[0].Click += StartGame_Click;
        buttons[1].Click += Settings_Click;
        buttons[2].Click += Exit_Click;
    }

    private void StartGame_Click(object sender, EventArgs e)
    {
        currentGameState = GameState.Playing;
        TankModel = new TankModel(new Vector2(100, 100), 0f, 100f, new Rectangle(0, 0, 800, 600), tankTexture);
        tankController = new TankController(TankModel, trollfaceModel, bulletController);
    }

    private void Settings_Click(object sender, EventArgs e)
    {
        currentGameState = GameState.Settings;
    }

    private void Exit_Click(object sender, EventArgs e)
    {
        Exit();
    }

    protected override void Update(GameTime gameTime)
    {
        var currentKeyboardState = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || (currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape)))
        {
            if (currentGameState == GameState.Playing || currentGameState == GameState.Settings)
            {
                currentGameState = GameState.MainMenu;
            }
            else if (currentGameState == GameState.MainMenu)
            {
                Exit();
            }
        }

        switch (currentGameState)
        {
            case GameState.MainMenu:
                foreach (var button in buttons)
                {
                    button.Update(gameTime);
                }
                break;

            case GameState.Playing:
                tankController.Update(gameTime);
                trollfaceController.Update(gameTime);
                bulletController.Update(gameTime);
                trollfaceController.UpdatePlayerPosition(TankModel.Position);
                if (TankModel.IsDestroyed)
                {
                    currentGameState = GameState.GameOver;
                    gameOverTime = gameTime.TotalGameTime.TotalSeconds;
                }
                break;

            case GameState.Settings:
                if (currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
                {
                    volumeLevel = Math.Min(volumeLevel + 0.1f, 1.0f);
                }
                if (currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
                {
                    volumeLevel = Math.Max(volumeLevel - 0.1f, 0.0f);
                }
                MediaPlayer.Volume = volumeLevel;
                break;

            case GameState.GameOver:
                if (gameTime.TotalGameTime.TotalSeconds - gameOverTime > 5)
                {
                    currentGameState = GameState.MainMenu;
                }
                break;
        }

        previousKeyboardState = currentKeyboardState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();

        switch (currentGameState)
        {
            case GameState.MainMenu:
                foreach (var button in buttons)
                {
                    button.Draw(spriteBatch);
                }
                break;

            case GameState.Playing:
                tankView.Draw(spriteBatch, TankModel);
                trollfaceView.Draw(spriteBatch, trollfaceModel);
                bulletView.Draw(bulletController.Bullets);
                break;

            case GameState.Settings:
                spriteBatch.DrawString(font, "Volume: " + volumeLevel, new Vector2(350, 300), Color.White);
                break;

            case GameState.GameOver:
                GraphicsDevice.Clear(Color.Red);
                spriteBatch.DrawString(font, "Game Over", new Vector2(350, 300), Color.White);
                break;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
}
