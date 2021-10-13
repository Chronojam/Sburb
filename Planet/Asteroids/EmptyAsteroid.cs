using Godot;

public class EmptyAsteroid : Planet {
    OpenSimplexNoise noiseGenerator;
    public EmptyAsteroid(OpenSimplexNoise noise) :
        base() 
    {
        noiseGenerator = noise;
    }

    public override void _Ready()
    {
        var cam = GetNode<Camera>("/root/Foo/Camera");
        var LODLevels = new float[]
        {
            64f, //* this.Scale.Length(),
            32f //* this.Scale.Length(),
        };
        for( int i = 0; i < 6; i++) {
            var thing = Faces[i];
            // 11
            var p = new AsteroidQuad(null, cam, 3, 0, GetCorners(thing.up, thing.forward, thing.right), new Color(1,1,1,1), LODLevels, noiseGenerator);
            p.Scale = Scale;
            p.Name = "TopLevel_" + i;
            AddChild(p);
        }
        AddChild(_inflence);     
    }
}