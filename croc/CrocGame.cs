using System;
using System.Net.Mime;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace croc;

public class CrocGame : Game {
    private bool DEBUG;
    
    //graphics stuff
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;    
    private int screenWidth = 1920;
    private int screenHeight = 1080;
    private Vector2 centre;
    
    private GameState _state = GameState.MenuScreen;
    private int currentTick = 0;

    private MouseState _mouseState;
    private DateTime timeWhenPressed;
    
    private Song song;
    
    //bg textures
    private Texture2D sky;
    private Texture2D hill;
    private Texture2D menuBG;
    
    //cursor textures
    private Texture2D normalCursor;
    private Texture2D hightlightedCursor;

    //entities
    //player
    private Player _player;
    
    //bush
    private Bush[] bushes = new Bush[5];
    
    //game font
    private SpriteFont _font;
    
    //button stuff
    private PlayButton _playButton;
    private static Texture2D buttonTexture;
    
    private void debugMessages() {
        Vector2 location = new Vector2(20, 120);
        Color colour = Color.Crimson;
        drawEquation(-1500, 5700, 460);
        _spriteBatch.DrawString(_font, "Current time: " + currentTick, location, colour);
        _spriteBatch.DrawString(_font, "Mouse position: " + _mouseState.Position, new Vector2(20, 140), colour);
        _spriteBatch.DrawString(_font, "Current scene: "+ _state, new Vector2(20, 160), colour);
        _spriteBatch.DrawString(_font, "Clickable: " + _player.clickable, new Vector2(20, 180), colour);
    }
    private void loadBackground() {
        sky = Content.Load<Texture2D>("daysky");
        hill = Content.Load<Texture2D>("hill");
        menuBG = Content.Load<Texture2D>("menu");
    }
    public CrocGame() {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = screenWidth;
        _graphics.PreferredBackBufferHeight = screenHeight;
        _graphics.ApplyChanges();
        
        centre = new Vector2(screenWidth / 2, screenHeight / 2);
        

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        for (int i = 0; i < bushes.Length; i++) {
            bushes[i] = new Bush(Content.Load<Texture2D>("bush"));
        }
        _player = new Player(Content.Load<Texture2D>("standing"));
        
        normalCursor = Content.Load<Texture2D>("cursor");
        hightlightedCursor = Content.Load<Texture2D>("selected");
        MouseCursor.FromTexture2D(normalCursor, 0, 0);

        base.Initialize();
    }

    
    
    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("file");
        _player.addWalkingTexture(Content.Load<Texture2D>("walking"));

        //music by matthew
        this.song = Content.Load<Song>("frogger");
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

        loadBackground();
    }
    
    void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e) {
        // 0.0f is silent, 1.0f is full volume
        MediaPlayer.Volume -= 0.1f;
        MediaPlayer.Play(song);
    }


    private void drawBackground() {
        _spriteBatch.Draw(sky, new Vector2(0,0), Color.White);
        _spriteBatch.Draw(hill, new Vector2(0,0), Color.White);
    }
    
    
    protected override void Update(GameTime gameTime) {
        _mouseState = Mouse.GetState();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.R) && DateTime.Now > timeWhenPressed.AddSeconds(0.2)) {
            timeWhenPressed = DateTime.Now;
            if (DEBUG) {
                DEBUG = false;
            } else {
                DEBUG = true;
            }
        }

        switch (_state) {
            case GameState.MenuScreen:
                updateMainMenu(gameTime);
                break;
            case GameState.HillLevel:
                updateGamePlay(gameTime);
                break;
            case GameState.GameOver:
                updateGameOver(gameTime);
                break;
        }
       
        
        base.Update(gameTime);
    }

    private void updateMainMenu(GameTime gameTime) {
        if (_playButton == null) {
            _playButton = new PlayButton(Content.Load<Texture2D>("buttontexture"), screenWidth/2, 700,  "PLAY GAME", _state);
        }
        else {
            _playButton.checkIfClicked(_mouseState, ref _state);
        }
        
    }

    private void updateGamePlay(GameTime gameTime) {
        _player.getInput((float) gameTime.ElapsedGameTime.TotalSeconds, _mouseState);
        
        if (currentTick == 600) {
            sky = Content.Load<Texture2D>("nightsky");
        } else if (currentTick == 1199) {
            sky = Content.Load<Texture2D>("daysky");
        }

        if (currentTick < 1200) {
            currentTick++;
        } else {
            currentTick = 0;
        }

        if (_player.clickable) {
            foreach (var bush in bushes) {
                bush.checkIfClicked(_mouseState, bush.onClick);
                
            }
        }
    }
    private void updateGameOver(GameTime gameTime) {
        throw new NotImplementedException();
    }


    protected void drawEquation(int n, int m, int c) {
        float x, y;
        Vector2 pos = new Vector2(0,0);
        var dot = Content.Load<Texture2D>("dot");

        x = 0;
        while (x < screenWidth) {
            y = (float )(Math.Pow(x + n, 2) / m) + c;
            pos.X = x;
            pos.Y = y;
            _spriteBatch.Draw(dot, pos, Color.White);
            x += (float) 0.2;
        }
    }

    

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        
        switch (_state) {
            case GameState.MenuScreen:
                drawMainMenu(gameTime);
                break;
            case GameState.HillLevel:
                drawGameplay(gameTime);
                break;  
            case GameState.GameOver:
                drawGameOver(gameTime);
                break;
        }

        if (DEBUG) { debugMessages(); }
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void drawGameplay(GameTime gameTime) {
        drawBackground();
        foreach (var bush in bushes) {
            bush.draw(_spriteBatch);
        }

        _player.draw(_spriteBatch);
    }

    
    
    private void drawMainMenu(GameTime gameTime) {
        
        currentTick = 1000;
        
        _spriteBatch.Draw(menuBG, System.Numerics.Vector2.Zero, Color.White);
        
        
        _playButton.drawButton(_spriteBatch, _font);
    }

    private void drawGameOver(GameTime gameTime) {
        throw new NotImplementedException();
    }
}
