using System;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace croc; 

public class Entity {
    public Vector2 position;
    public Texture2D texture;
    public Hitbox hitbox;
    public string info = "Nothing to see here!";

    public void setPosition(Vector2 p) {
        this.position = p;
        
    }

    public string getInfo() {
        return info;
    }

    public Entity(Texture2D t, Vector2 p) {
        this.texture = t;
        this.position = p;
        this.hitbox = new Hitbox(new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height));
    }

    public Entity(Texture2D t) {
        this.texture = t;
    }

    protected Entity() {
        throw new NotImplementedException();
    }

    public void draw(SpriteBatch s) {
        s.Draw(texture, position, Color.White);
    }
    
    public void setRandomPosition() {
        Random rand = new Random();
        float minY, x, y;
        int maxY = 1080 - texture.Height;
        x = rand.Next(0, 1920);
        minY = (float) (Math.Pow(x - 1500, 2) / 5700) + 460;
        y = rand.Next((int) minY, maxY);
        this.position = new Vector2(x, y);
    }
    
    public void checkIfClicked(MouseState ms, Action someMethod) {
        Vector2 mouse_position = new Vector2(ms.X, ms.Y);
        if (hitbox.hitbox.Contains(mouse_position) && ms.LeftButton == ButtonState.Pressed) {
            someMethod();
        }
        
    }
}