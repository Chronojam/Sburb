using Godot;

public class PlanetaryInfluence : Area {
    public PlanetaryInfluence(Planet p) {
        this.Name = p.Name + "_Influence"; 

        // Pull things in our area towards us.
        this.GravityPoint = true;
        this.Gravity = 9.8f;
        this.Monitoring = true;
        this.SpaceOverride = Area.SpaceOverrideEnum.Replace;
    }

    public override void _Ready()
    {
        this.GravityVec = this.GlobalTransform.origin;

        var colShape = new CollisionShape();
        colShape.Scale = GetParent<Spatial>().Scale * 2;
        colShape.Shape = new SphereShape();
        AddChild(colShape);
        base._Ready();
    }
}