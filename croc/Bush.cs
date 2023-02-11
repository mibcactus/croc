using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace croc; 

public class Bush : Entity {
    public Bush(Texture2D t) : base(t) {
        this.texture = t;
        setPosition();
    }

    private void setPosition() {
        Random rand = new Random();
        float minY, x, y;
        int maxY = 1080 - texture.Height;
        x = rand.Next(0, 1920);
        minY = (float) (Math.Pow(x - 1500, 2) / 5700) + 460;
        y = rand.Next((int) minY, maxY);
        this.position = new Vector2(x, y);
    }
}