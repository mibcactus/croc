using Microsoft.Xna.Framework;

namespace croc; 

public class GameUtil {
    public static bool colliding(Entity entity1, Entity entity2) {
        Rectangle a = entity1.hitbox.hitbox;
        Rectangle b = entity2.hitbox.hitbox;

        return false;
    }
}