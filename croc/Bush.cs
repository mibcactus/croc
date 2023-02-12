using System;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace croc; 

public class Bush : Entity {
    private string[] infoPossibilities = new[] {"It's a bush...", "It's green.", "I spent too long putting this bush in the right place.", "It's a bush!"};
    private string info;
    public Bush(Texture2D t) : base(t) {
        this.texture = t;
        setRandomPosition();
    }

    public new string getInfo() {
        Random rand = new Random();
        return infoPossibilities[rand.Next(infoPossibilities.Length)];
    }

    public void setInfo() {
        Random rand = new Random();
        info = infoPossibilities[rand.Next(infoPossibilities.Length)];
    }
    
    public void onClick() {
        TextBox tb = new TextBox(texture, info);
    }
    
}