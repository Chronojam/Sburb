using Godot;
using Godot.Collections;

public class Skaia : Planet {
    OpenSimplexNoise noiseGenerator;

    public Skaia(int seed) :
        base() 
    {
        this.Name = "Skaia";
        noiseGenerator = new OpenSimplexNoise();
        noiseGenerator.Seed = seed;
        noiseGenerator.Octaves = 4;
        noiseGenerator.Period = 0.7f;
        noiseGenerator.Persistence = .5f;
    }

    public override void _Ready()
    {
        var cam = GetNode<Camera>("/root/Foo/Camera");
        var LODLevels = new float[]
        {
            64f * this.Scale.Length(),
            32f * this.Scale.Length(),
            16f * this.Scale.Length(),
            8f * this.Scale.Length(),
        };

        for( int i = 0; i < 6; i++) {
            var thing = Faces[i];
            var p = new SkaiaQuad(GetCorners(thing.up, thing.forward, thing.right), new Array<Image>(), noiseGenerator);
            p.Scale = Scale;
            p.Name = "TopLevel_" + i;
            AddChild(p);
        }

        AddChild(_influence);
    }
}