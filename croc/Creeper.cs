using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace croc; 

public class Creeper : Entity {
    private Texture2D[] textures;

    public Creeper(Texture2D t1, Texture2D t2, Texture2D t3) {
        textures[0] = t1;
        textures[1] = t2;
        textures[2] = t3;
        setRandomPosition();
    }

    //public void draw(SpriteBatch s) { }
}