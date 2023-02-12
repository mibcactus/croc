using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace croc; 

public class Bush : Entity {
    //private bool action = false;
    private DateTime timeSinceInteractedWith;
    private string[] textOptions = {"It's a bush...", "It's green.", "Okay?", "It's a bush!", "I spent too\nlong on these damn bushes." };
    Random rand = new Random();
    private String str;

    public Bush(Texture2D t) : base(t) {
        this.texture = t;
        setRandomPosition();
    }

    public void checkIfClicked(MouseState ms) {
        Vector2 mouse_position = new Vector2(ms.X, ms.Y);
        if (hitbox.Contains(mouse_position) && ms.LeftButton == ButtonState.Pressed && DateTime.Now > timeSinceInteractedWith.AddSeconds(0.5)) {
            Console.WriteLine("There is a bush here");
            timeSinceInteractedWith = DateTime.Now;
            str = textOptions[rand.Next(textOptions.Length)];
        }
        
    }
    
    public void draw(SpriteBatch s, SpriteFont f) {
        s.Draw(texture, position, Color.White);
        Vector2 stringPosition = position;
        
        if (DateTime.Now < timeSinceInteractedWith.AddSeconds(2)) {
            stringPosition.Y += texture.Height;
            s.DrawString(f, str, stringPosition, Color.Black);
        }
        
        
    }

}