using Godot;
using Godot.Collections;
/*
    Quad
      -> Mesh
      -> Child
          -> Quad
          -> Quad
          -> Quad

*/

public enum QuadPosition {
    TopLeft = 0,
    TopRight = 1,
    BottomLeft = 3,
    BottomRight = 2,
}

public class Quad : Spatial
{
    public float[] LODLevels { get; private set; }
    protected const int CHILD_COUNT = 4;
    protected MeshInstance MeshNode;
    protected Spatial ChildNode;


    // LODPoints is a set of positions from 
    // which to measure LOD Distances from
    protected Spatial[] LODPoints;
    
    public int Size { get; private set; }
    public int Depth { get; private set; }
    protected Quad Parent;
    protected Spatial LODTarget;
    protected Vector3 centreOfMesh;
    public Vector3[] Corners { get; private set; }
    protected Vector3[][] childCorners;

    protected Array<Image> Samples = new Array<Image>();

    protected Vector2 MinUVBounds;
    protected Vector2 MaxUVBounds;


    // Subdivide/merge vote


    public Quad(Vector3[] corners, Array<Image> samples) {
        this.Size = 11;
        this.Depth = 0;
        this.MinUVBounds = new Vector2(0, 0);
        this.MaxUVBounds = new Vector2(1, 1);
        this.Samples = samples;
        this.Parent = null;
        this.Corners = corners;
    }

    public Quad(Quad parent, QuadPosition position, Vector3[] corners) {
        this.Parent = parent;
        this.Size = parent.Size;
        this.Depth = parent.Depth + 1;
        this.LODTarget = parent.LODTarget;
        this.LODLevels = parent.LODLevels;
        this.Samples = parent.Samples;
        (this.MinUVBounds, this.MaxUVBounds) = PopulateUVBounds(position, parent);
        this.Corners = corners;
    }

    static (Vector2, Vector2) PopulateUVBounds(QuadPosition p, Quad Parent) {
        var HalfWayParent = (Parent.MaxUVBounds + Parent.MinUVBounds) / 2;
        return p switch {
            // 0,0 ; 0.5,0.5
            QuadPosition.TopLeft => (Parent.MinUVBounds, HalfWayParent),
            // 0.5,0 ; 1,0.5
            QuadPosition.TopRight => (
                new Vector2(HalfWayParent.x, Parent.MinUVBounds.y), 
                new Vector2(Parent.MaxUVBounds.x, HalfWayParent.y)
            ),
            QuadPosition.BottomLeft => (
                new Vector2(Parent.MinUVBounds.x, HalfWayParent.y),
                new Vector2(HalfWayParent.x, Parent.MaxUVBounds.y)
            ),
            QuadPosition.BottomRight => (
                new Vector2(HalfWayParent.x, HalfWayParent.y), 
                Parent.MaxUVBounds
            ),
        };
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // We're doing this in here because Scale and GetNode are a function
        // of actually being in the scene.

        if (Parent == null) {
            // Also only run this if we dont have a parent, if we do
            // then these should've been populated via the constructor.
            this.LODTarget = GetNode<Spatial>("/root/Foo/Camera");
            if (this.LODTarget == null) {
                throw new System.Exception("WTF?");
            }
            this.LODLevels = new float[] {
                64f * this.Scale.Length(),
                32f * this.Scale.Length(),
                16f * this.Scale.Length(),
                8f * this.Scale.Length(),
            };
        }

        if (MeshNode == null) {
            MeshNode = new MeshInstance();
            MeshNode.Name = Name + "_Mesh";
            MeshNode.MaterialOverride = ResourceLoader.Load<ShaderMaterial>("res://Planet/Default.tres");
        }

        ChildNode = new Spatial();
        ChildNode.Name = Name + "_Children";

        // Decide our LOD levels
        AddChild(MeshNode);
        AddChild(ChildNode);

        // Normal faces have 5
        int LOD_POINT_COUNT = 5;

        // Lower depth faces have 9
        if (Depth == 0) {
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
        LODPoints[0].Translate(Corners[0]);
        LODPoints[1].Translate(Corners[1]);
        LODPoints[2].Translate(Corners[2]);
        LODPoints[3].Translate(Corners[3]);
        
        // Just get the camera for now.
        var vertices = ConstructMesh();
        
        var halfSizeIndex = (Size -1) / 2;
        var fullSizeIndex = (Size -1);

        // Add the centre position as a condition too.
        LODPoints[4].Translate(vertices[Index(halfSizeIndex,halfSizeIndex)]);

        // At lower depths, add additional points. At these depths, the quads can be *quite*
        // large, so this allows the target to approach from an edge.
        if (Depth == 0) {
            LODPoints[5].Translate(vertices[Index(halfSizeIndex, 0)]);
            LODPoints[6].Translate(vertices[Index(0, halfSizeIndex)]);
            LODPoints[7].Translate(vertices[Index(fullSizeIndex, 0)]);
            LODPoints[8].Translate(vertices[Index(0, fullSizeIndex)]);
        }
    }


    private bool voteToMerge;
    public override void _Process(float delta)
    {
        if (!MeshNode.Visible || LODTarget == null || LODPoints.Length == 0)
            return;

        var target = LODTarget.GlobalTransform.origin;
        var mergeVote = 0;
        foreach(Spatial lodPoint in LODPoints) {
            var distance = Mathf.Abs(lodPoint.GlobalTransform.origin.DistanceTo(target));
            // distance > parent's LOD
            if (Parent != null && distance > LODLevels[Depth - 1] + 10) {
                mergeVote++;
                continue;
            }
            // distance < child's LOD
            if (LODLevels.Length > Depth + 1 && distance < LODLevels[Depth + 1] - 10) {
                Subdivide();
                // If we choose to subdivide, theres no point going
                // any further.
                return;
            }

        }

        // Cant merge if you dont have a parent!
        if (Parent == null)
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
            Parent.Merge();
        }
    }

    // Overrideable function for planets to implement specific vertex deformations
    // Use this to also add additional features on a given vertex, i.e place an object or so
    public virtual float VertexDeformation(Vector3 vertex, Point iteration) {
        // Calculate UV value here for a given sample ?
        // Return a deformation value here.
        // Also add models here, use this.Depth to determine what LOD level to add.
        return 1f;
    }

    private Vector3 PointOnCubeToPointOnSphere(Vector3 p) {

        return p.Normalized();

        float x2 = p.x * p.x;
        float y2 = p.y * p.y;
        float z2 = p.z * p.z;

        float x = Mathf.Sqrt(1 - (y2 + z2) / 2 + (y2 * z2) /3);
        float y = Mathf.Sqrt(1 - (z2 + x2) / 2 + (z2 * x2) /3);
        float z = Mathf.Sqrt(1 - (x2 + y2) / 2 + (x2 * y2) /3);

        return new Vector3(x, y, z);
    }

    private Vector3[] ConstructMesh()
    {
        // (1, 0, 0) -> (0, 0, -1)
        Vector3[] vertices = new Vector3[Size * Size];
        Vector3[] normals = new Vector3[Size * Size];
        Vector2[] uvs = new Vector2[Size * Size];
        Color[] colors = new Color[Size * Size];
        int[] triangles = new int[(Size - 1) * (Size - 1) * 6];

        var halfSizeIndex = (Size -1) / 2;
        var fullSizeIndex = (Size -1);

        int triIndex = 0;

        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                int i = Index(x, y);
                Vector2 percent = new Vector2(x, y) / (Size - 1 );
                var vector01 =Corners[0].LinearInterpolate(Corners[1], percent.x);
                var vector32 = Corners[3].LinearInterpolate(Corners[2], percent.x);
                Vector3 pointOnCube = vector01.LinearInterpolate(vector32, percent.y);
                Vector3 pointOnUnitSphere = PointOnCubeToPointOnSphere(pointOnCube);
                float deformFactor = VertexDeformation(pointOnUnitSphere, new Point(x, y));
                vertices[i] = pointOnUnitSphere * deformFactor;
                normals[i] = pointOnUnitSphere;

                // This UV is the value of any textures and stuff.
                var uvWeight = new Vector2(x, y) / (Size - 1); // 0.0 -> 1.0
                var uvValue = MinUVBounds.LinearInterpolate(MaxUVBounds, uvWeight);
                uvs[i] = uvValue;
                colors[i] = new Color(1,1,1,1);


                if (x != Size - 1 && y != Size - 1)
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

        // martindevans@gmail.com
        // ping him when it breaks
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
        return x + y * Size;
    }

    public virtual void GenerateChildren() {
        ChildNode.AddChild(new Quad(this, QuadPosition.TopLeft, childCorners[0]) { Name = "Child_TopLeft"});
        ChildNode.AddChild(new Quad(this, QuadPosition.TopRight, childCorners[1]) { Name = "Child_TopRight"});
        ChildNode.AddChild(new Quad(this, QuadPosition.BottomRight, childCorners[3]) { Name = "Child_BottomRight"});
        ChildNode.AddChild(new Quad(this, QuadPosition.BottomLeft, childCorners[2]) { Name = "Child_BottomLeft"});
    }
    
    private void Subdivide()
    {
        MeshNode.Visible = false;
        ChildNode.Visible = true;

        // Check if the child node already has children, if it does
        // then simply set it to visible instead.
        if (ChildNode.GetChildCount() > 0) {
            return;
        }
        // Otherwise, generate all the children.
        GenerateChildren();
    }
    private void Merge() 
    {
        // Dont remove, hide all the children instead.
        ChildNode.Visible = false;
        MeshNode.Visible = true;
    }
}
