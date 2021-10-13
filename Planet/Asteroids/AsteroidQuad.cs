using Godot;
public class AsteroidQuad : Quad {
    OpenSimplexNoise noiseGenerator;
    public AsteroidQuad(Quad parent, Spatial LODTarget, int size, int depth, Vector3[] corners, Color color, float[] LODLevels, OpenSimplexNoise noiseGenerator) : 
        base(parent, LODTarget, size, depth, corners, color, LODLevels) 
    {
        // Make sure we call the base constructor to assign everything.
        this.noiseGenerator = noiseGenerator;
    }

    public override void _Ready()
    {
        MeshNode = new MeshInstance();
        MeshNode.Name = Name + "_Mesh";
        MeshNode.MaterialOverride = ResourceLoader.Load<ShaderMaterial>("res://Planet/Asteroids/Asteroid.tres");
        base._Ready();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    public override Vector3[] ConstructMesh()
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
                float noise = noiseGenerator.GetNoise3d(pointOnUnitSphere.x, pointOnUnitSphere.y, pointOnUnitSphere.z); // [0 - 2]
                vertices[i] = pointOnUnitSphere * (1 + (noise * 1.5f));
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

    public override void Subdivide()
    {
        for (int i = 0; i < CHILD_COUNT; i ++) {
            var child = new AsteroidQuad(this, _lodtarget, _size, _depth + 1, childCorners[i], ChildColors[i], _LODLevels, noiseGenerator);
            child.Name = "Child_" + i;
            ChildNode.AddChild(child);
        }
        MeshNode.Visible = false;
    }
    public override void Merge() 
    {
        foreach (Node child in ChildNode.GetChildren()) {
            child.QueueFree();
            ChildNode.RemoveChild(child);
        }
        MeshNode.Visible = true;
    }

}