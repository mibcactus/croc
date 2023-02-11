using System;
using System.Net.Mime;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    
    private int currentTick = 0;

    private MouseState _mouseState;
    private DateTime timeWhenPressed;

    //bg textures
    private Texture2D sky;
    private Texture2D hill;
    
    //bush
    private Bush[] bushes = new Bush[5];
    
    //game font
    private SpriteFont _font;
    
    private void debugMessages() {
        Vector2 location = new Vector2(20, 120);
        Color colour = Color.Crimson;
        drawEquation(-1500, 5700, 460);
        _spriteBatch.DrawString(_font, "Current time: " + currentTick, location, colour);
        _spriteBatch.DrawString(_font, "Mouse position: " + _mouseState.Position, new Vector2(20, 140), colour);
        //_spriteBatch.DrawString(_font, "Bush position: "+ bush.position, new Vector2(20, 140), colour);
    }
    private void loadBackground() {
        sky = Content.Load<Texture2D>("daysky");
        hill = Content.Load<Texture2D>("hill");
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
        
        
        // bushes = new Entity[bushAmount](Content.Load<Texture2D>("bush"), new Vector2(rand.Next(200, 1000), rand.Next(100, 1500) ) );
        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("file");
        
        loadBackground();
    }


    private void drawBackground() {
        _spriteBatch.Draw(sky, new Vector2(0,0), Color.White);
        _spriteBatch.Draw(hill, new Vector2(0,0), Color.White);
    }
    
    
    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }

        _mouseState = Mouse.GetState();

        if (_mouseState.LeftButton == ButtonState.Pressed && DateTime.Now > timeWhenPressed.AddSeconds(0.2)) {
            timeWhenPressed = DateTime.Now;
            if (DEBUG) {
                DEBUG = false;
            } else {
                DEBUG = true;
            }
        }

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
        
        base.Update(gameTime);
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

        drawBackground();
        foreach (var bush in bushes) {
            bush.draw(_spriteBatch);
        }


        if (DEBUG) { debugMessages(); }
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
