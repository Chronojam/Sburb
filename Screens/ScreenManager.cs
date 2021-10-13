using Godot;

public class ScreenManager : Node
{
    public override void _Ready()
    {
        LoadScreen("res://Screens/MainMenu/Scenes/Menu.tscn");
    }

    public T LoadScreen<T>(string path) where T : Godot.Node {
        return (T)LoadScreen(path);
    }
    public Node LoadScreen(string path) {
        // Only allow a single screen at the moment
        foreach ( Node thing in GetChildren()) {
            thing.QueueFree();
        }
        var screen = ResourceLoader.Load<PackedScene>(path);
        var inst = screen.Instance();
        AddChild(inst);

        return inst;
    }
}
