using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace croc; 

public class Player : Entity {
    private Texture2D walking;
    private Single scale = 0.2f;
    private Single rotation = 0f;
    public State state;
    private DateTime timeSinceStateChange;
    Texture2D current;

    private int speed = 500;
    
    public Player(Texture2D t) : base(t) {
        this.texture = t;
        this.current = texture;
        this.position = new Vector2(700, 900);
        state = State.STANDING;
    }

    public void addWalkingTexture(Texture2D t) {
        this.walking = t;
    }

    private DateTime timeSinceSpriteChange;
    
    public new void draw(SpriteBatch s) {
        if (state == State.MOVING) {
            if (current == texture && DateTime.Now > timeSinceSpriteChange.AddSeconds(0.5)) {
                current = walking;
                timeSinceSpriteChange = DateTime.Now;
            } else if (current == walking && DateTime.Now > timeSinceSpriteChange.AddSeconds(0.5)) {
                timeSinceSpriteChange = DateTime.Now;
                current = texture;
            }
        }
        
        s.Draw(current, position, null, drawColour, rotation, Vector2.Zero, scale, SpriteEffects.None, 1f);
    }
    
    
    public void getInput(float elapsedGameTime, MouseState ms) {
        float distance = speed * elapsedGameTime;

        bool moved = false;
        if (Keyboard.GetState().IsKeyDown(Keys.A)) {
            position.X -= distance;
            moved = true;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D)) {
            position.X += distance;
            moved = true;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.W)) {
            position.Y -= distance;
            moved = true;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S)) {
            position.Y += distance;
            moved = true;
        }

        if (moved & (state == State.STANDING)) {
            state = State.MOVING;
            timeSinceStateChange = DateTime.Now;
        } else if (!moved & (state == State.MOVING)){
            state = State.STANDING;
            current = texture;
            timeSinceStateChange = DateTime.Now;
        }
        
        
        CheckPosition(1920);
        clickable = inClickingArea(ms);
    }

    public void CheckPosition(int widthBound) {
        float h = texture.Height * scale;
        float w = texture.Width * scale;
        if (position.X > widthBound - w / 2) {
            position.X = (widthBound - w / 2);
        }
        else if (position.X < 0 - w / 2) {
            position.X = 0 - (w / 2);
        }

        int upperHeightBound = (int) (Math.Pow(position.X - 1500, 2) / 5700) + 460;
        
        if (position.Y > 1080 - (h / 2)) {
            position.Y = 1080 - (h / 2);
        }
        else if (position.Y < upperHeightBound - (h / 2)) {
            position.Y = upperHeightBound - (h / 2);
        }
    }

    private Color drawColour = Color.White;
    
    public bool clickable;
    int radius = 300;
    public bool inClickingArea(MouseState ms) {
        double g;

        g = Math.Pow((ms.X - position.X), 2) + Math.Pow((ms.Y - position.Y), 2);
        g = Math.Sqrt(g);

        if (g < radius) {
            drawColour = Color.Blue;
            return true;
        }
        else {
            drawColour = Color.White;
            return false;
        }

    }

}