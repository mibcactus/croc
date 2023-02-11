using System.Drawing;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace croc; 

public class Entity {
    public Vector2 position;
    public Texture2D texture;
    public Hitbox hitbox;
    public int hp;

    public void setPosition(Vector2 p) {
        this.position = p;
        
    }

    public Entity(Texture2D t, Vector2 p) {
        this.texture = t;
        this.position = p;
        this.hitbox = new Hitbox(new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height));
    }
}