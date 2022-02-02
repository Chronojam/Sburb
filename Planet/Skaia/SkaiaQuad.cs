using Godot;
using Godot.Collections;
public class SkaiaQuad : Quad {
    OpenSimplexNoise NoiseGenerator;

    public SkaiaQuad(Vector3[] corners, Array<Image> samples, OpenSimplexNoise noise) :
        base(corners, samples) 
        {
            this.NoiseGenerator = noise;
        }
    public SkaiaQuad(SkaiaQuad parent, QuadPosition position,  Vector3[] corners) : 
        base(parent, position, corners) 
    {
        this.NoiseGenerator = parent.NoiseGenerator;
    }

    public override void _Ready()
    {
        MeshNode = new MeshInstance();
        MeshNode.Name = Name + "_Mesh";
        MeshNode.MaterialOverride = ResourceLoader.Load<ShaderMaterial>("res://Planet/Skaia/Skaia.tres");
        base._Ready();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    public override float VertexDeformation(Vector3 vertex, Point iteration)
    {
        var noise = NoiseGenerator.GetNoise3d(vertex.x, vertex.y, vertex.z);
        return (1 + (noise *.25f));
    }

    public override void GenerateChildren()
    {
        for (int i = 0; i < CHILD_COUNT; i ++) {
            ChildNode.AddChild(new SkaiaQuad(this, (QuadPosition)i, childCorners[i]) { Name = "Child_" + (QuadPosition)i});
        }
    }
}