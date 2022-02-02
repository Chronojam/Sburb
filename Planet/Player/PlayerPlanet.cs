
using Godot;
using Godot.Collections;
using System;

/*
* R = HeightModifers
* G = Buildings; Forge, House, Scratch, Quest Bed
* B = 
* A = 
*/

public class PlayerPlanetMaskGenerator {
    public Dictionary<Vector3, Image> Images = new Dictionary<Vector3, Image>() {
        { Vector3.Up, new Image() },
        { Vector3.Down, new Image() },
        { Vector3.Left, new Image() },
        { Vector3.Right, new Image() },
        { Vector3.Forward, new Image() },
        { Vector3.Back, new Image() },
    };

    public PlayerPlanetMaskGenerator(RandomNumberGenerator n, Color[] Landmarks) {
        Dictionary<int,Array<Color>> landmarkPositions = new Dictionary<int,Array<Color>>();
        foreach (Color c in Landmarks) {
            // Decide what face to put this landmark on.
            int thing = n.RandiRange(0, 5);
            if (landmarkPositions.ContainsKey(thing)) {
                landmarkPositions[thing].Add(c);
            } else {
                landmarkPositions[thing] = new Array<Color>() {
                    c
                };
            }
        }

        Console.WriteLine($"{landmarkPositions}");

        int tileNum = 0;
        foreach( var item in Images ) {
            var img = new Image();
            img.Create(2048, 2048, false, Image.Format.Rgba8);
            img.Fill(new Color(0.0f,0.0f,0.0f,1.0f));
            img.Lock();

            // 
            if (landmarkPositions.ContainsKey(tileNum)) {
                foreach (Color c in landmarkPositions[tileNum]) {
                    var x = n.RandiRange(50, (int)img.GetSize().x - 51);
                    var y = n.RandiRange(50, (int)img.GetSize().y - 51);
                    img.SetPixel(x, y, c);
                    Console.WriteLine($"{x} {y} {c}");
                }
            }

            img.SavePng($"C:\\Users\\cgard\\Documents\\GodotProjects\\WorldNouns\\Planet\\Player\\Face-{tileNum}.png");
            Images[item.Key] = img;
            tileNum++;
        }
    }
}

public class RoadsModifier : IPlanetaryModifier
{
    VField2D _field;
    PackedScene _roadTemplate;
    Array<Point> _roadPositions = new Array<Point>();
    public RoadsModifier(VField2D f, RandomNumberGenerator n) {
        _field = f;
        // Pick a point in the field to start the roads
        var x = n.RandiRange(0, _field.SizeX - 1);
        var y = n.RandiRange(0, _field.SizeY - 1);

        BuildRoads(new Point(x, y), 15);

        _roadTemplate = ResourceLoader.Load<PackedScene>("res://Planet/Player/Roads/Road.tscn");
    }
    private void BuildRoads(Point p, int roadLength) {
        if (!_roadPositions.Contains(p)) {
            _roadPositions.Add(p);
            Console.WriteLine($"{p.X} {p.Y}");
        }
        
        // Neighbours
        var left = new Point(p.X - 1, p.Y);
        var right = new Point(p.X + 1, p.Y);
        var up = new Point(p.X, p.Y + 1);
        var down = new Point(p.X, p.Y - 1);

        Point[] neighbours = new Point[8] {
            left,
            right,
            up,
            down,
            up + left,
            up + right,
            down + left,
            down + right,
        };

        if (!_field.Contains(p)) {
            return;
        }

        var selfMagnitude = _field.Get(p);
        Vector2 bestMagnitude = Vector2.Zero;
        Point bestNeighbour = p;

        foreach (Point n in neighbours) {
            if (!_field.Contains(n)) {
                continue;
            }
            var theirMagnitude = _field.Get(n);
            if (selfMagnitude.Dot(theirMagnitude) > 0.0f) {
                // Facing the same direction
                if (theirMagnitude > bestMagnitude) {
                    bestNeighbour = n;
                }
            }
        }
        if (bestNeighbour == p) {
            return;
        }
        
        if (roadLength > 0) {
            BuildRoads(bestNeighbour, roadLength - 1);
        }
    }


    public void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        if (parent.Depth == 0) {
            // We're at minimum depth.
            // We need to transform the iteration coords to planet-coords
            // we can inspect the parent here and find what face we're on
            var xoffset = 0;
            var yoffset = 0;

        
            if (parent.Corners[0] == new Vector3(1, 1, -1)) { // up
                xoffset = parent.Size;
                yoffset = parent.Size;
            }
            if (parent.Corners[0] == new Vector3(-1, -1, 1)) { // down
                xoffset = parent.Size;
                yoffset = parent.Size * 3;
            }
            if (parent.Corners[0] == new Vector3(-1, 1, -1)) { // left
                xoffset = 0;
                yoffset = parent.Size;
            }
            if (parent.Corners[0] == new Vector3(1, -1, 1)) { // right
                xoffset = parent.Size * 2;
                yoffset = parent.Size;
            }
            if (parent.Corners[0] == new Vector3(-1, -1, -1)) { // forward
                xoffset = parent.Size;
                yoffset = 0;
            }
            if (parent.Corners[0] == new Vector3(1, -1, 1)) { // back
                xoffset = parent.Size;
                yoffset = parent.Size * 2;
            }

            var key = new Point(iteration.X + xoffset, iteration.Y + yoffset);
            // This is fucking gross but
            var found = false;
            foreach (Point p in _roadPositions) {
                if (p.X == key.X && p.Y == key.Y) {
                    found = true;
                    break;
                }
            }
            if (!found) {
                return;
            }
            //_roadPositions[key] = roadPos;
            // Sample from _field, decide if we want to place a road here..
            // For now, lets just try placing some basic 'blocks'
            var r = _roadTemplate.Instance<Spatial>();

            var t = r.Transform;
            t.origin = position;
            r.Transform = t;

            parent.AddChild(r);
        }
    }
}

public class PlayerPlanet : Planet {
    public static Dictionary<string, Color> PossibleLandmarks = new Dictionary<string, Color>() {
        { "House", new Color(0,1.0f,0,0) },
        { "Forge", new Color(0,2.0f,0,0) },
        { "ScratchConstruct", new Color(0,3.0f,0,0)},
    };

    PlayerPlanetMaskGenerator mask;
    RandomNumberGenerator randomNumberGenerator;
    OpenSimplexNoise noiseGenerator;
    IPlanetaryModifier[] modifiers;
    public PlayerPlanet(int seed, PlayerInfo playerInfo) {
        
        // Defaults
        randomNumberGenerator = new RandomNumberGenerator();
        randomNumberGenerator.Seed = (ulong)seed;

        noiseGenerator = new OpenSimplexNoise();
        noiseGenerator.Seed = seed;
        noiseGenerator.Octaves = 4;
        noiseGenerator.Period = 0.7f;
        noiseGenerator.Persistence = .5f;

        var rGrid = new VField2DGrid(11 * 4, 5);

        modifiers = new IPlanetaryModifier[] {
            playerInfo.SelectedElementSynonym,
            playerInfo.SelectedAspectSynonym,
            new RoadsModifier(rGrid, randomNumberGenerator),
        };

        // Allow Element and Aspect to alter noise and number generation
        playerInfo.SelectedElementSynonym.ConstructionMod(randomNumberGenerator, noiseGenerator);
        playerInfo.SelectedAspectSynonym.ConstructionMod(randomNumberGenerator, noiseGenerator);

        mask = new PlayerPlanetMaskGenerator(randomNumberGenerator, new Color[3]{
            PossibleLandmarks["House"],
            PossibleLandmarks["Forge"],
            PossibleLandmarks["ScratchConstruct"]
        });

        // Name the planet.
        var first = playerInfo.SelectedElementSynonym.Name;
        var second = playerInfo.SelectedAspectSynonym.Name;

        this.Name = $"Land of {first} and {second}";
    }

    public override void _Ready() {
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
            // 11
            var p = new PlayerPlanetQuad(
               GetCorners(thing.up, thing.forward, thing.right),
               new Array<Image>() {
                   mask.Images[thing.up]
               },
               noiseGenerator,
               modifiers);
            p.Scale = Scale;
            p.Name = "TopLevel_" + i;
            AddChild(p);
        }

        AddChild(_influence);
    }
};


