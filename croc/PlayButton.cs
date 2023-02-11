using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace croc; 

public class PlayButton : Button {
    public PlayButton(int x, int y, int width, int height, string l, GameState state) : base(x, y, width, height, l) { }
    public PlayButton(Texture2D t, int x, int y, int width, int height, string l, GameState state) : base(t, x, y, width, height, l) { }

    public void checkIfClicked(MouseState ms, ref GameState state) {
        Vector2 mouse_position = new Vector2(ms.X, ms.Y);
        if (shape.Contains(mouse_position) && ms.LeftButton == ButtonState.Pressed) {
            state = GameState.HillLevel;
        }
        
    }
}