using Godot;
using System;

public abstract class Planet : Spatial
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
    public Planet() {
        Defaults();
    }

    private void Defaults() {
        _inflence = new PlanetaryInfluence(this);
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
        var cam = GetNode<Camera>("/root/Foo/Camera");
        if (cam != null) {
            Console.WriteLine("Found Camera");
        }

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
            var p = new Quad(null, cam, 3, 0, GetCorners(thing.up, thing.forward, thing.right), new Color(1,1,1,1), LODLevels);
            p.Scale = Scale;
            p.Name = "TopLevel_" + i;
            AddChild(p);
        }

        AddChild(_inflence);
    }
}
