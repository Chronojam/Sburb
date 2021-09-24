using Godot;
using System;

// PlayerInfo is just the information about a given player.
// represented as a node so we can use godot's networking features.
public class PlayerInfo : Node {
    public PlayerClass Class;
    public PlayerAspect Aspect;
    public ClassicalElement Element;

    public PlayerInfo(PlayerClass c, PlayerAspect a, ClassicalElement e) {
        this.Class = c;
        this.Aspect = a;
        this.Element = e;

        Console.WriteLine(this.Title);
    }

    string Title { 
        get {
            return $"${Class.Name} of ${Aspect.Name}";
        }
    }
}