using Godot;

public class ServerEditorObject : Spatial
{

    // Can this support other things on top of it?
    [Export]
    public bool IsSupportingStructure = false;

    [Export]
    public string EditorAreaSupportPath = "StaticBody/EditorAreaSupport";

    [Export]
    public string EditorAreaPath = "StaticBody/EditorArea";

    public Area EditorAreaSupport {get; private set; }
    public Area EditorArea { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        EditorArea = GetNode<Area>(EditorAreaPath);
        EditorAreaSupport = GetNode<Area>(EditorAreaSupportPath);
    }

    public void DropEditorComponents() {
        EditorAreaSupport.QueueFree();
        EditorArea.QueueFree();
    }

    public void SetColor(Color color) {
        var node = GetNode<MeshInstance>("StaticBody/MeshInstance");
        var material = (SpatialMaterial)node.MaterialOverride;
        // Check if the material actually exists here you noggin.
        if (material == null) {
            material = new SpatialMaterial();
        }
        material.FlagsTransparent = true;
        material.AlbedoColor = color;
        node.MaterialOverride = material;
    }
}
