using Godot;

public class Skaia : Planet {
    // its skaia

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
        // Optional seed here!
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


        // TODO different planets need different quad implementations
        // How do?
        for( int i = 0; i < 6; i++) {
            var thing = Faces[i];
            // 11
            var p = new SkaiaQuad(null, cam, 11, 0, GetCorners(thing.up, thing.forward, thing.right), new Color(1,1,1,1), LODLevels, noiseGenerator);
            p.Scale = Scale;
            p.Name = "TopLevel_" + i;
            AddChild(p);
        }

        AddChild(_inflence);
    }
}