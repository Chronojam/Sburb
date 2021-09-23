using System.Collections.Generic;
using Godot;

public class StandardSession : Session
{
    const float prospitRadius = 40f;
    const float playerRadius = 100f;
    const float ringRadius = 190f;
    const float derseRadius = 230f;

    RandomNumberGenerator random = new RandomNumberGenerator();

    public StandardSession() {
        var p = new List<PlayerInfo>();
        p.Add(new PlayerInfo());
        p.Add(new PlayerInfo());
        p.Add(new PlayerInfo());
        p.Add(new PlayerInfo());
        _constructMedium(p);
    }
    public StandardSession(List<PlayerInfo> _playerInfos) {
        _constructMedium(_playerInfos);
    }

    private void _constructMedium(List<PlayerInfo> _playerInfos) {
        // First set up the basics of a session.
        var Skaia = new Skaia();
        var ProspitOrbit = new Spatial();
        var Prospit = new Prospit();

        var Derse = new Derse();
        var DerseOrbit = new Spatial();

        Skaia.Scale = new Vector3(30, 30, 30);

        // Prospit is our next planet out, put this in orbit of skaia.
        Prospit.Translate(Vector3.Left * Skaia.Scale * prospitRadius);
        Prospit.Scale = new Vector3(10,10,10);
        ProspitOrbit.AddChild(Prospit);

        float step = 360.0f / _playerInfos.Count;
        float stepVal = 0f;

        var planetSpacer = new Spatial();
        AddChild(planetSpacer);

        // Add the player's planets
        foreach (PlayerInfo p in _playerInfos) {
            // Do stuff with playerinfo to create a unique planet for
            // each player.
            //planetSpacer.Rotate(Vector3.Up, stepRadians);

            var stepRadians = (stepVal / 180) * Mathf.Pi;
            var planet = new PlayerPlanet();
            planetSpacer.AddChild(planet);

            planet.Translate(Vector3.Left.Rotated(Vector3.Up, stepRadians) * Skaia.Scale * playerRadius);
            planet.Scale = new Vector3(15, 15, 15);

            stepVal += step;
        }

        // Add the outer ring
        var OuterRing = _generateOuterRing(500, Skaia.Scale);

        // Then finally derse outside the outerring
        Derse.Translate(Vector3.Left * Skaia.Scale * derseRadius);
        Derse.Scale = new Vector3(10,10,10);
        DerseOrbit.AddChild(Derse);

        AddChild(Skaia);
        AddChild(ProspitOrbit);
        AddChild(OuterRing);
        AddChild(DerseOrbit);
    }

    private Node _generateOuterRing(int count, Vector3 skaiaScale) {
        // First create an empty spatial entity
        // Which will act as our 'middle'
        var orbit = new Spatial();
        orbit.GlobalTransform = Transform.Identity;

        // The ectobiology lab is always added.
        var ectoLab = new EctobiologyLab();
        ectoLab.Translate(Vector3.Left * skaiaScale * ringRadius);
        ectoLab.Scale = new Vector3(2.5f, 2.5f, 2.5f);
        orbit.AddChild(ectoLab);

        // And so are frog temples, TODO here.

        float step = 360.0f / count;
        float stepVal = 10f;
        for (int i = 0; i < count; i++) {
            // Todo asteroids.
            var randStep = stepVal + this.random.RandfRange(-0.1f, 0.1f);
            var randRingRadius = ringRadius + this.random.RandfRange(-15, 0);
            var randScale = this.random.RandfRange(1.5f, 4f);
            var randHeight = this.random.RandfRange(-75,75);
            var stepRadians = Mathf.Deg2Rad(stepVal);
            var roid = new EmptyAsteroid();

            orbit.AddChild(roid);
            roid.Translate(Vector3.Left.Rotated(Vector3.Up, stepRadians) * skaiaScale * randRingRadius);
            roid.Translate(new Vector3(0, randHeight, 0));
            roid.Scale = Vector3.One * randScale;

            stepVal += step;
        }
        return orbit;
    }
}