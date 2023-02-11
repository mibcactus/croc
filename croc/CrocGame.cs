using System.Net.Mime;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace croc;

public class Game1 : Game {
    private int screenWidth = 1920;
    private int screenHeight = 1080;
    private Vector2 centre;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private bool DEBUG = true;
    
    private int currentTick = 0;

    //bg textures
    private Texture2D sky;
    private Texture2D hill;

    //game font
    private SpriteFont _font;
    
    private void debugMessages() {
        drawCurrentTick();
    }
    private void drawCurrentTick() {
        _spriteBatch.DrawString(_font, "Current time: " + currentTick, new Vector2(20,120), Color.Crimson);
    }
    private void loadBackground() {
        sky = Content.Load<Texture2D>("daysky");
        hill = Content.Load<Texture2D>("hill");
    }
    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = screenWidth;
        _graphics.PreferredBackBufferHeight = screenHeight;
        _graphics.ApplyChanges();
        centre = new Vector2(screenWidth / 2, screenHeight / 2);
        

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
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


    

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();

        drawBackground();


        if (DEBUG) { debugMessages(); }
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
