using Godot;
using System;

// A 2 Dimensional Vector Field
public class VField2D
{
    protected Vector2[,] _field;
    public int SizeX {
        get {
            return _field.GetLength(0);
        }
    }
    public int SizeY {
        get {
            return _field.GetLength(1);
        }
    }

    public VField2D(int size) {
        _field = new Vector2[size, size];
    }
    public VField2D(int sizeX, int sizeY) {
        _field = new Vector2[sizeX, sizeY];
    }
    public VField2D(Vector2[,] field) {
        _field = field;
        _normalizeField();
    }

    public Vector2 Get(int x, int y) {
        return _field[x, y];
    }
    public Vector2 Get(Point p) {
        return _field[p.X, p.Y];
    }

    public bool Contains(Point p) {
        return p.X >= 0 && 
                p.Y >= 0 &&
                p.X < SizeX - 1 &&
                p.Y < SizeY - 1; 
    }

    public VField2D AddVector (Vector2 v) {
        Vector2[,] newField = new Vector2[_field.GetLength(0), _field.GetLength(1)];
        for(int x = 0; x < _field.GetLength(0) - 1; x++) {
            for (int y = 0; y < _field.GetLength(1) - 1; y++) {
                newField[x,y] = _field[x,y] + v;
            }
        }
        return new VField2D(newField);
    }
    public VField2D AddField (VField2D f) {
        if (f._field.GetLength(0) != _field.GetLength(0) ||
            f._field.GetLength(1) != _field.GetLength(1)) {
                throw new System.Exception("Mismatched Vector field dimensions");
            }
        Vector2[,] newField = new Vector2[_field.GetLength(0), _field.GetLength(1)];
        for(int x = 0; x < _field.GetLength(0) - 1; x++) {
            for (int y = 0; y < _field.GetLength(1) - 1; y++) {
                newField[x,y] = _field[x,y] + f._field[x,y];
            }
        }
        return new VField2D(newField);
    }
    public VField2D MultiplyField (VField2D f) {
        if (f._field.GetLength(0) != _field.GetLength(0) ||
            f._field.GetLength(1) != _field.GetLength(1)) {
                throw new System.Exception("Mismatched Vector field dimensions");
            }
        Vector2[,] newField = new Vector2[_field.GetLength(0), _field.GetLength(1)];
        for(int x = 0; x < _field.GetLength(0) - 1; x++) {
            for (int y = 0; y < _field.GetLength(1) - 1; y++) {
                newField[x,y] = _field[x,y] * f._field[x,y];
            }
        }
        return new VField2D(newField);
    }
    public VField2D DivideField (VField2D f) {
        if (f._field.GetLength(0) != _field.GetLength(0) ||
            f._field.GetLength(1) != _field.GetLength(1)) {
                throw new System.Exception("Mismatched Vector field dimensions");
            }
        Vector2[,] newField = new Vector2[_field.GetLength(0), _field.GetLength(1)];
        for(int x = 0; x < _field.GetLength(0) - 1; x++) {
            for (int y = 0; y < _field.GetLength(1) - 1; y++) {
                newField[x,y] = _field[x,y] / f._field[x,y];
            }
        }
        return new VField2D(newField);
    }

    public static VField2D operator +(VField2D f) => f;
    public static VField2D operator -(VField2D f) => f.AddVector(-Vector2.One);
    public static VField2D operator +(VField2D a, VField2D b) => a.AddField(b);
    public static VField2D operator -(VField2D a, VField2D b) => a.AddField(-b);
    public static VField2D operator *(VField2D a, VField2D b) => a.MultiplyField(b);
    public static VField2D operator /(VField2D a, VField2D b) => a.DivideField(b);

    public virtual void _fillField() {
        
        // By default, fill the field with Vector2.Zero
        for(int x = 0; x < _field.GetLength(0) - 1; x++) {
            for (int y = 0; y < _field.GetLength(1) - 1; y++) {
                _field[x,y] = new Vector2(0.1f, 0.1f);
            }
        }
    }
    private void _normalizeField() {
        for(int x = 0; x < _field.GetLength(0) - 1; x++) {
            for (int y = 0; y < _field.GetLength(1) - 1; y++) {
                _field[x,y] = _field[x,y].Normalized();
            }
        }
    }

    public void Print() {
        for (int y = 0; y < _field.GetLength(1); y++) {
            string s = "";
            for (int x = 0; x < _field.GetLength(0); x++) {
                s += (" " + _field[x, y].ToString());
            }
            Console.WriteLine(s);
        }
    }
}
