using Godot;

public class Prospit : CityPlanet {
    const float moonRadius = 20f;
    CityPlanet moon;
    Spatial moonOrbit;
    public Prospit() {
        this.Name = "Prospit";

        // Also create a moon
        this.moon = new CityPlanet();
        this.moonOrbit = new Spatial();
        this.moon.Name = "Prospit's Moon";
        // Moon is approx 1/3 size of prospit proper
        this.moon.Translate(Vector3.Left * moonRadius);
        this.moon.Scale = new Vector3(2.2f, 2.2f, 2.2f); 
        this.moonOrbit.AddChild(this.moon);

        AddChild(moonOrbit);
    }

    public override void _Process(float delta)
    {
        moonOrbit.Rotate(Vector3.Up, delta * 0.1f);
        base._Process(delta);
    }
}