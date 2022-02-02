using Godot;
using Godot.Collections;
public class PlayerPlanetQuad : Quad {
    OpenSimplexNoise noiseGenerator;
    IPlanetaryModifier[] modifiers;
    
    public PlayerPlanetQuad(Vector3[] corners, Array<Image> samples, OpenSimplexNoise noise, IPlanetaryModifier[] modifiers) :
        base(corners, samples)
    {
        this.noiseGenerator = noise;
        this.modifiers = modifiers;
    }
    public PlayerPlanetQuad(PlayerPlanetQuad parent, QuadPosition position, Vector3[] corners) : 
        base(parent, position, corners) 
    {
        // Make sure we call the base constructor to assign everything.
        this.noiseGenerator = parent.noiseGenerator;
        this.modifiers = parent.modifiers;
    }

    public override void _Ready()
    {
        // Base mesh material.
        MeshNode = new MeshInstance();
        MeshNode.Name = Name + "_Mesh";
        MeshNode.MaterialOverride = ResourceLoader.Load<ShaderMaterial>("res://Planet/Player/Player.tres");

        base._Ready();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    public override float VertexDeformation(Vector3 vertex, Point iteration)
    {
        // Here we want to allow the aspect/element synonym to add various objects
        // i.e Mind -> Thought should be able to place the neuron structures onto the planet's
        // surface here.
        // thing.QuadMod(this, vertex);

        var noise = noiseGenerator.GetNoise3d(vertex.x, vertex.y, vertex.z);
        noise = (1 + (noise *.25f));

        foreach(IPlanetaryModifier modifier in modifiers) {
            // Use the modified vertex location here, as we need
            // to construct models etc on the surface of the planet
            // rather than the default 'circle' plane.
            modifier.QuadMod(this, vertex * noise, iteration); 
        }

        // Sample from any images we've been given for this quad
        // and apply as needed.
        /*foreach (Image s in Samples) {
            s.Lock(); // ?
            var uvWeight = iteration / (Size - 1); // 0.0 -> 1.0
            var uvValue = MinUVBounds.LinearInterpolate(MaxUVBounds, uvWeight);
            var pixelLoc = (s.GetSize() - Vector2.One) * uvValue;
            var color = s.GetPixelv(pixelLoc); // 0.0 -> 2048 ex.
            
            // Use color.r to deform the vertex
            noise = (noise + (color.r));

            if (pixelLoc.x > 1920) {
                Console.WriteLine($"{color}");
            }
            // Use color.g to add any bits
            if (color.g > 0.0f) {
                // Just add loads for now.
                Console.WriteLine("Sampling Green");
                noise = (noise + (100f));
            }
        }*/

        return noise;
    }
    public override void GenerateChildren()
    {
        for (int i = 0; i < CHILD_COUNT; i ++) {
            ChildNode.AddChild(new PlayerPlanetQuad(this, (QuadPosition)i, childCorners[i]) { Name = "Child_" + (QuadPosition)i});
        }
    }
}