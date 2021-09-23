using Godot;

// PlayerInfo is just the information about a given player.
// represented as a node so we can use godot's networking features.
public class PlayerInfo : Node {
    PlayerClass _class;
    PlayerAspect _aspect;

    string Title { 
        get {
            return $"${this._class.Name} of ${this._aspect.Name}";
        }
    }
}