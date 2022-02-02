using Godot;
using Godot.Collections;

public abstract class Planet : Spatial
{
    protected (Vector3 up, Vector3 forward, Vector3 right)[] Faces = {
        (Vector3.Up, Vector3.Forward, Vector3.Right),
        (Vector3.Down, Vector3.Left, Vector3.Back),
        (Vector3.Left, Vector3.Forward, Vector3.Up),
        (Vector3.Right, Vector3.Down, Vector3.Back),
        (Vector3.Forward, Vector3.Left, Vector3.Down),
        (Vector3.Back, Vector3.Right, Vector3.Down)
    };

    protected PlanetaryInfluence _influence;
    public Planet() {
        _influence = new PlanetaryInfluence(this);
    }

    public Vector3[] GetCorners(Vector3 a, Vector3 b, Vector3 c)  {
        return new Vector3[4] {
            (a + b + c),
            (a - b + c),
            (a - b - c),
            (a + b - c), 
        };
    }

    public override void _Ready()
    {
        // TODO different planets need different quad implementations
        // How do?
        for( int i = 0; i < 6; i++) {
            var thing = Faces[i];
            // 11
            var p = new Quad(GetCorners(thing.up, thing.forward, thing.right), new Array<Image>());
            p.Scale = Scale;
            p.Name = "TopLevel_" + i;
            AddChild(p);
        }
        AddChild(_influence);
    }
}
