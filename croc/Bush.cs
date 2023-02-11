using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace croc; 

public class Bush : Entity {
    public Bush(Texture2D t) : base(t) {
        this.texture = t;
        setRandomPosition();
    }
}