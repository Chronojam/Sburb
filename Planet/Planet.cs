using Godot;
using System;

[Tool]
public class Planet : Spatial
{
    (Vector3 up, Vector3 forward, Vector3 right)[] Faces = {
        (Vector3.Up, Vector3.Forward, Vector3.Right),
        (Vector3.Down, Vector3.Left, Vector3.Back),
        (Vector3.Left, Vector3.Forward, Vector3.Up),
        (Vector3.Right, Vector3.Down, Vector3.Back),
        (Vector3.Forward, Vector3.Left, Vector3.Down),
        (Vector3.Back, Vector3.Right, Vector3.Down)
    };

    private String _name;
    private PlanetaryInfluence _inflence;

    private OpenSimplexNoise _noise;

    public Planet() {
        Defaults();
    }

    public Planet(Noun one, Noun two) {
        _name = "Land of " + one.Name + " and " + two.Name;
        Defaults();
    }

    private void Defaults() {
        _inflence = new PlanetaryInfluence(this);

        _noise = new OpenSimplexNoise();
        _noise.Octaves = 4;
        _noise.Period = 20.0f;
        _noise.Persistence = 0.8f;
    }

    public Vector3[] GetCorners(Vector3 a, Vector3 b, Vector3 c)  {
        return new Vector3[4] {
            (a + b + c),
            (a - b + c),
            (a - b - c),
            (a + b - c), 
        };
    }
    public override void _Process(float delta)
    {
        if (Engine.EditorHint)
        {
            foreach(Node child in GetChildren()) {
                child._Process(delta);
            }
        }
    }
    public override void _Ready()
    {
        var cam = GetNode<Camera>("../Camera");
        if (cam != null) {
            Console.WriteLine("Found Camera");
        }
        Quad.LODLevels = new float[]
        {
            3200f,
            1600f,
            800f,
            400f,
            200f,
            100f,
            50f,
        };

        for( int i = 0; i < 6; i++) {
            var thing = Faces[i];
            var p = new Quad(null, cam, 11, 0, GetCorners(thing.up, thing.forward, thing.right), new Color(1,1,1,1));
            p.Scale = Scale;
            p.Name = "TopLevel_" + i;
            AddChild(p);
        }

        AddChild(_inflence);
    }
}
