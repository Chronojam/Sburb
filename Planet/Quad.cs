using Godot;

/*
    Quad
      -> Mesh
      -> Child
          -> Quad
          -> Quad
          -> Quad

*/
public class Quad : Spatial
{
    //public static Dictionary<int, float> LODLevels;
    private float[] _LODLevels;
    static Color[] ChildColors = new Color[4] {
        new Color(1, 0, 0, 1),
        new Color(0, 1, 0, 1),
        new Color(0, 0, 1, 1),
        new Color(1, 1, 0, 1)
    };

    const int CHILD_COUNT = 4;
    MeshInstance MeshNode;
    Spatial ChildNode;
    Spatial[] LODPoints;
    

    private int _size;
    private Color _color;
    private int _depth;
    private Quad _parent;
    private Spatial _lodtarget;
    private Vector3 centreOfMesh;
    private Vector3[] _corners;
    private Vector3[][] childCorners;

    // Subdivide/merge vote
    private bool voteToMerge;
        
    public Quad(Quad parent, Spatial LODTarget, int size, int depth, Vector3[] corners, Color color, float[] LODLevels) {
        this._size = size;
        this._color = color;
        this._depth = depth;
        this._parent = parent;
        this._corners = corners;
        this._lodtarget = LODTarget;
        this._LODLevels = LODLevels;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MeshNode = new MeshInstance();
        MeshNode.Name = Name + "_Mesh";
        MeshNode.MaterialOverride = ResourceLoader.Load<ShaderMaterial>("res://Planet/Default.tres");

        ChildNode = new Spatial();
        ChildNode.Name = Name + "_Children";

        // Decide our LOD levels
        AddChild(MeshNode);
        AddChild(ChildNode);

        // Normal faces have 5
        int LOD_POINT_COUNT = 5;

        // Lower depth faces have 9
        if (_depth == 0) {
            LOD_POINT_COUNT = 9;
        }

        LODPoints = new Spatial[LOD_POINT_COUNT];

        for( int i = 0; i < LOD_POINT_COUNT; i++) {
            LODPoints[i] = new Spatial();
            LODPoints[i].Name = "LOD_" + i;
            AddChild(LODPoints[i]);
        }

        // Set the lod positions to the corners of the
        // mesh, Use translate to account for scale.
        LODPoints[0].Translate(_corners[0]);
        LODPoints[1].Translate(_corners[1]);
        LODPoints[2].Translate(_corners[2]);
        LODPoints[3].Translate(_corners[3]);
        
        // Just get the camera for now.
        var vertices = ConstructMesh();
        
        var halfSizeIndex = (_size -1) / 2;
        var fullSizeIndex = (_size -1);

        // Add the centre position as a condition too.
        LODPoints[4].Translate(vertices[Index(halfSizeIndex,halfSizeIndex)]);

        // At lower depths, add additional points. At these depths, the quads can be *quite*
        // large, so this allows the target to approach from an edge.
        if (_depth == 0) {
            LODPoints[5].Translate(vertices[Index(halfSizeIndex, 0)]);
            LODPoints[6].Translate(vertices[Index(0, halfSizeIndex)]);
            LODPoints[7].Translate(vertices[Index(fullSizeIndex, 0)]);
            LODPoints[8].Translate(vertices[Index(0, fullSizeIndex)]);
        }
    }

    public override void _Process(float delta)
    {
        if (!MeshNode.Visible || _lodtarget == null || LODPoints.Length == 0)
            return;

        var target = _lodtarget.GlobalTransform.origin;
        var mergeVote = 0;
        foreach(Spatial lodPoint in LODPoints) {
            var distance = Mathf.Abs(lodPoint.GlobalTransform.origin.DistanceTo(target));
            // distance > parent's LOD
            if (_parent != null && distance > _LODLevels[_depth - 1] + 10) {
                mergeVote++;
                continue;
            }
            // distance < child's LOD
            if (_LODLevels.Length > _depth + 1 && distance < _LODLevels[_depth + 1] - 10) {
                Subdivide();
                // If we choose to subdivide, theres no point going
                // any further.
                return;
            }

        }

        // Cant merge if you dont have a parent!
        if (_parent == null)
            return;

        // Only merge if we're out of range of every point.
        if (mergeVote == LODPoints.Length) {
            voteToMerge = true;
        }
        // Get all our parent's children, and see if theres a consensus
        // to merge, including us, this should possibly be a function
        // on the parent?
        int consensus = 0;
        foreach(Quad child in GetParent().GetChildren()) {
            if (child.voteToMerge) {
                consensus++;
            }
        }
        if (consensus == GetParent().GetChildCount()) {
            _parent.Merge();
        }
        //base._Process(delta);
    }
    public Vector3[] ConstructMesh()
    {
        // (1, 0, 0) -> (0, 0, -1)
        Vector3[] vertices = new Vector3[_size * _size];
        Vector3[] normals = new Vector3[_size * _size];
        Vector2[] uvs = new Vector2[_size * _size];
        Color[] colors = new Color[_size * _size];
        int[] triangles = new int[(_size - 1) * (_size - 1) * 6];

        var halfSizeIndex = (_size -1) / 2;
        var fullSizeIndex = (_size -1);

        int triIndex = 0;

        for (int y = 0; y < _size; y++)
        {
            for (int x = 0; x < _size; x++)
            {
                int i = Index(x, y);
                Vector2 percent = new Vector2(x, y) / (_size - 1 );
                var vector01 =_corners[0].LinearInterpolate(_corners[1], percent.x);
                var vector32 = _corners[3].LinearInterpolate(_corners[2], percent.x);
                Vector3 pointOnCube = vector01.LinearInterpolate(vector32, percent.y);
                Vector3 pointOnUnitSphere = pointOnCube.Normalized();
                vertices[i] = pointOnUnitSphere;
                normals[i] = pointOnUnitSphere;
                uvs[i] = new Vector2(x, y);
                colors[i] = _color;


                if (x != _size - 1 && y != _size - 1)
                {
                    triangles[triIndex] = Index(x, y);
                    triangles[triIndex + 1] = Index(x + 1, y);
                    triangles[triIndex + 2] = Index(x, y + 1);

                    triangles[triIndex + 3] = Index(x + 1, y);
                    triangles[triIndex + 4] = Index(x + 1, y + 1);
                    triangles[triIndex + 5] = Index(x, y + 1); 
                    triIndex += 6;
                }
            }
        }

        childCorners = new Vector3[4][] {
            new Vector3[4] { // Red
                vertices[Index(0,0)],
                vertices[Index(halfSizeIndex,0)],
                vertices[Index(halfSizeIndex,halfSizeIndex)],
                vertices[Index(0,halfSizeIndex)]
            },
            new Vector3[4] { // Green
                vertices[Index(halfSizeIndex,0)],
                vertices[Index(fullSizeIndex,0)],
                vertices[Index(fullSizeIndex,halfSizeIndex)],
                vertices[Index(halfSizeIndex,halfSizeIndex)]
            },
            new Vector3[4] { // Blue
                vertices[Index(halfSizeIndex,halfSizeIndex)],
                vertices[Index(fullSizeIndex,halfSizeIndex)],
                vertices[Index(fullSizeIndex,fullSizeIndex)],
                vertices[Index(halfSizeIndex,fullSizeIndex)]
            },
            new Vector3[4] { // Yellow
                vertices[Index(0,halfSizeIndex)],
                vertices[Index(halfSizeIndex,halfSizeIndex)],
                vertices[Index(halfSizeIndex,fullSizeIndex)],
                vertices[Index(0,fullSizeIndex)]
            },
        };

        

        Godot.Collections.Array arr = new Godot.Collections.Array();
        arr.Resize((int)ArrayMesh.ArrayType.Max);

        arr[((int)ArrayMesh.ArrayType.Color)] = colors;
        arr[((int)ArrayMesh.ArrayType.Vertex)] = vertices;
        arr[((int)ArrayMesh.ArrayType.Index)] = triangles;
        arr[((int)ArrayMesh.ArrayType.Normal)] = normals;
        arr[((int)ArrayMesh.ArrayType.TexUv)] = uvs;

        ArrayMesh arrMesh = new ArrayMesh();
        arrMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arr);

        MeshNode.Mesh = arrMesh;
        MeshNode.CreateTrimeshCollision(); // Add some collisions
        return vertices;
    }
    public int Index(int x, int y) {
        return x + y * _size;
    }
    public void Subdivide()
    {
        for (int i = 0; i < CHILD_COUNT; i ++) {
            var child = new Quad(this, _lodtarget, _size, _depth + 1, childCorners[i], ChildColors[i], _LODLevels);
            child.Name = "Child_" + i;
            ChildNode.AddChild(child);
        }
        MeshNode.Visible = false;
    }
    public void Merge() 
    {
        foreach (Node child in ChildNode.GetChildren()) {
            child.QueueFree();
            ChildNode.RemoveChild(child);
        }
        MeshNode.Visible = true;
    }
}
