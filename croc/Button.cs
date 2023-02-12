using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace croc; 

public abstract class Button {
    public Rectangle shape;
    private Vector2 position;
    private Texture2D texture;
    private String label;

    public Button(int x, int y, int width, int height, String l) {
        shape = new Rectangle(new Point(x, y), new Point(width, height));
        position = new Vector2(x, y);
        label = l;
    }

    public Button(Texture2D t, int x, int y, String l) {
        int posX, posY;
        texture = t;
        label = l;
        shape = new Rectangle(new Point(x, y), new Point(texture.Width, texture.Height));
        posX = x + (texture.Width / 4);
        posY = y + (texture.Height/2);
        position = new Vector2(posX, posY);
        
    }

    public void drawButton(SpriteBatch s, SpriteFont f) {
        s.Draw(texture, shape, Color.Yellow);
        s.DrawString(f, label, position, Color.Black);
    }

}