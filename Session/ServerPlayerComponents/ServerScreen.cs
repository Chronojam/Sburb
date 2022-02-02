using Godot;
using Godot.Collections;
using System; 

// ServerScreenFloor is responsible for actually drawing the grid.
// And also the container for the objects in that floor (for now);
public class ServerScreenFloor : ImmediateGeometry {
    Array<Vector2> Points = new Array<Vector2>();

    private float _alpha = 1f; 
    private int _size = 1;
    public ServerScreenFloor() {

    }
    public ServerScreenFloor(int size) {
        var _endX = size;
        var _endZ = size;
        var _startX = -(_endX);
        var _startZ = -(_endZ);

        _size = size;

        for(var z = _startZ; z < _endZ; z++) {
            Points.Add(new Vector2(_startX, z));
            Points.Add(new Vector2(_endX, z));
        }
        for(var x = _startX; x < _endX; x++) {
            Points.Add(new Vector2(x, _startZ));
            Points.Add(new Vector2(x, _endZ));
        }
    }


    public void SetAlpha(float amount) {
        this._alpha = amount;
        foreach( Node child in GetChildren()) {
            if (child is ServerEditorObject) {
                var thing = (ServerEditorObject)child;
                thing.SetColor(new Color(1,1,1,amount));
            }
        }
    }
    public override void _Process(float delta)
    {
        Clear();
        Begin(PrimitiveMesh.PrimitiveType.Lines);
        SetColor(new Color(1,1,1, _alpha));
        for (var i = 0; i < Points.Count; i+=2) {
            // Add the previous point if it exists
            if (i + 1 > Points.Count - 1) {
                continue;

            }
            var pnt = Points[i];
            var next = Points[i+1];
            AddVertex(new Vector3(pnt.x, Transform.origin.y, pnt.y));       
            AddVertex(new Vector3(
                    next.x,
                    Transform.origin.y,
                    next.y
                ));
        }
        End();
        base._Process(delta);
    }
}


public class ServerScreen : Spatial
{
    [Export]
    public int FloorCount = 10;

    Array<PackedScene> Tiles = new Array<PackedScene>();

    private Array<Plane> Floors = new Array<Plane>();
    private Array<ServerScreenFloor> Grids = new Array<ServerScreenFloor>();
    private int _size = 10;
    private int _height = 2;
    private int _selectedFloor;
    private ServerEditorObject _selectedTile;
    private int _selectedTileId;
    private bool _hasLocalCollisions = false;
    private bool _isSupported = true;


    public ServerScreen() {
        // Load up all our gubbins
        var ps = ResourceLoader.Load<PackedScene>("res://Session/ServerPlayerComponents/BuildingComponents/BuildingBlockExample.tscn");
        Tiles.Add(ps);
    }

    private Camera _camera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _selectedFloor = 1;
        GetNode("UI/Splitter/FloorSelector/Up").Connect("pressed", this, "FloorUp");
        GetNode("UI/Splitter/FloorSelector/Down").Connect("pressed", this, "FloorDown");

        _camera = GetNode<Camera>("Camera");
        for( var i = 1; i < FloorCount; i ++) {
            var p = new Plane();
            p.Normal = new Vector3(0, 1, 0);
            p.D = _height * i;
            Floors.Add(p);

            var d = new ServerScreenFloor(_size);
            d.Translate(new Vector3(0, _height * i, 0));
            d.Visible = false;
            Grids.Add(d);
            AddChild(d);
        }

        SelectFloor(1);
        SelectBuilding(0);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("mouse_0_pressed") && !_hasLocalCollisions && _isSupported) {
            var thing = Tiles[_selectedTileId].Instance<ServerEditorObject>();
            thing.Transform = _selectedTile.Transform;
            thing.DropEditorComponents();

            Grids[_selectedFloor].AddChild(thing);
        }
        base._Input(@event);
    }

    public override void _PhysicsProcess(float delta)
    {
        var rl = 1;
        var mp = GetViewport().GetMousePosition();
        var from = _camera.ProjectRayOrigin(mp);
        var to = _camera.ProjectRayNormal(mp) * rl;
        var cursorPosition = Floors[_selectedFloor].IntersectRay(from, to);

        if (cursorPosition != null) {
            var x = Mathf.CeilToInt(cursorPosition.Value.x);
            var z = Mathf.CeilToInt(cursorPosition.Value.z);

            if (_selectedTile != null) {
                var t = _selectedTile.Transform;
                t.origin = new Vector3(x, Grids[_selectedFloor].Transform.origin.y, z);
                _selectedTile.Transform = t;

                // TODO check if we've got supporting structures underneath.

            }
        }

        base._PhysicsProcess(delta);
    }


#region UIFunctions
    public void FloorUp() {
        if (_selectedFloor + 1 > FloorCount) {
            return;
        }
        SelectFloor(_selectedFloor + 1);
    }
    public void FloorDown() {
        if (_selectedFloor - 1 < 0) {
            return;
        }
        SelectFloor(_selectedFloor - 1);
    }
    public void SelectBuilding(int id) {
        if (_selectedTile != null) {
            _selectedTile.QueueFree();
        }
        _selectedTileId = id;
        _selectedTile = Tiles[id].Instance<ServerEditorObject>();

        Grids[_selectedFloor].AddChild(_selectedTile);

        // Give our 'cursor' building a green texture for now.
        EvaluateColor();
        _selectedTile.EditorArea.Connect("body_entered", this, "Collision_BodyEntered");
        _selectedTile.EditorArea.Connect("body_exited", this, "Collision_BodyExited");

        _selectedTile.EditorAreaSupport.Connect("body_entered", this, "Support_BodyEntered");
        _selectedTile.EditorAreaSupport.Connect("body_exited", this, "Support_BodyExited");

        
    }
    public void SelectFloor(int id) {
        foreach(ServerScreenFloor flr in Grids) {
            flr.Visible = false;
            flr.SetAlpha(1.0f);
        }
        _selectedFloor = id;
        if (id == 1) {
            // If we're the ground floor, we're presumed to always be supported
            _isSupported = true;
        } else {
            _isSupported = false;
        }
        Grids[_selectedFloor].Visible = true;
        Grids[_selectedFloor].SetAlpha(1.0f);
        if (_selectedFloor - 1 > 0) {
            Grids[_selectedFloor - 1].Visible = true;
            Grids[_selectedFloor - 1].SetAlpha(0.5f);
        }
        SelectBuilding(_selectedTileId);
    }

#endregion

#region CollisionChecking
    private void Support_BodyEntered(Node body) 
    {
        var seo = body as ServerEditorObject;
        if (seo == null) {
            return;
        }
        if (!_isSupported && seo.IsSupportingStructure) 
        {
            _isSupported = true;
            EvaluateColor();
        }
    }
    private void Support_BodyExited(Node body) 
    {
        if (_selectedTile.EditorAreaSupport.GetOverlappingBodies().Count == 0) {
            _isSupported = false;
            EvaluateColor();
        }
    }
    public void Collision_BodyEntered(Node body) {
        // Make sure we're not overlapping with anything.
        _hasLocalCollisions = true;
        EvaluateColor();
    }
    public void Collision_BodyExited(Node body) {
        if (_selectedTile.EditorArea.GetOverlappingBodies().Count == 0) {
            _hasLocalCollisions = false;
            EvaluateColor();
        }
    }
    public void EvaluateColor() {
        Console.WriteLine($"EvalColor: {_isSupported} {_hasLocalCollisions}");
        if (_isSupported && !_hasLocalCollisions) {
            _selectedTile.SetColor(new Color(0, 1, 0, 0.7f));
            return;
        }
        _selectedTile.SetColor(new Color(1,0, 0, 0.7f));
    }
#endregion

}
