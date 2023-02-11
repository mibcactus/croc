﻿using System;
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

    private int speed = 1000;
    
    public Player(Texture2D t) : base(t) {
        this.texture = t;
        this.current = texture;
        this.position = new Vector2(700, 900);
        state = State.STANDING;
    }
    
    public void draw(SpriteBatch s) {
        if (state == State.MOVING) {
            if (current == texture && DateTime.Now > timeSinceStateChange.AddSeconds(0.1)) {
                current = walking;
            } else if (current == walking && DateTime.Now > timeSinceStateChange.AddSeconds(0.1)) {
                current = texture;
            }
        }
        else {
            current = texture;
        }
        
        s.Draw(current, position, null, Color.White, rotation, Vector2.Zero, scale, SpriteEffects.None, 1f);
    }

    public void getInput(float elapsedGameTime) {
        float distance = speed * elapsedGameTime;

        if (Keyboard.GetState().IsKeyDown(Keys.A)) {
            position.X -= distance;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D)) {
            position.X += distance;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.W)) {
            position.Y -= distance;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S)) {
            position.Y += distance;
        }
        
        
        CheckPosition(1920);
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

}