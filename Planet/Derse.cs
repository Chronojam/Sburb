using Godot;

public class Derse : CityPlanet {

    const float moonRadius = 20f;
    CityPlanet moon;
    Spatial moonOrbit;
    public Derse() {
        this.Name = "Derse";

        this.moon = new CityPlanet();
        this.moonOrbit = new Spatial();
        this.moon.Name = "Derse's Moon";
        // Moon is approx 1/3 size of prospit proper
        this.moon.Translate(Vector3.Left * moonRadius);
        this.moon.Scale = new Vector3(2.2f, 2.2f, 2.2f); 
        this.moonOrbit.AddChild(this.moon);

        AddChild(moonOrbit);
    }

    public override void _Process(float delta)
    {
        moonOrbit.Rotate(Vector3.Up, delta * -0.1f);
        base._Process(delta);
    }
}