using Godot;

public class VField2DGrid : VField2D {

    private int _spacing;
    public VField2DGrid(int size, int spacing) :
        base(size) {
            _spacing = spacing;
            _fillField();
        }

    public override void _fillField()
    {
        // Get a load of zeros first.
        base._fillField();

        for( int x = 0; x < _field.GetLength(0); x++) {

            for (int y = 0; y < _field.GetLength(1); y++) {
                if (x % _spacing == 0) {
                    _field[x, y] += Vector2.Down;
                } 
                if (y % _spacing == 0) {
                    _field[x, y] += Vector2.Right;
                }
            }
        }
    }
}