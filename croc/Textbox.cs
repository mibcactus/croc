using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace croc; 

public class TextBox {
    private Button closeBox;
    private Rectangle shape;
    private Texture2D texture;
    private String text;
    Point location = new Point(50, 800);
    Point size = new Point(1820, 200);

    public TextBox(Texture2D t, String str) {
        text = str;
        texture = t;
        shape = new Rectangle(location, size);
        closeBox = new Button(texture, location.X, location.Y, "CLOSE");
        Console.WriteLine("Textbox created");
    }

    public void draw(SpriteBatch s, SpriteFont f, MouseState ms) {
        s.Draw(texture, shape, Color.White);
        Vector2 textPosition = new Vector2(location.X + 20, location.Y+20);
        s.DrawString(f, text, textPosition, Color.White);
        closeBox.drawButton(s, f);
    }

}