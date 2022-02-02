using Godot;
using Godot.Collections;

public class EmptyAsteroid : Planet {
    OpenSimplexNoise noiseGenerator;
    public EmptyAsteroid(OpenSimplexNoise noise) :
        base() 
    {
        noiseGenerator = noise;
    }

    public override void _Ready()
    {
        for( int i = 0; i < 6; i++) {
            var thing = Faces[i];
            // 11
            var p = new AsteroidQuad(GetCorners(thing.up, thing.forward, thing.right), new Array<Image>(), noiseGenerator);
            p.Scale = Scale;
            p.Name = "TopLevel_" + i;
            AddChild(p);
        }
        AddChild(_influence);     
    }
}