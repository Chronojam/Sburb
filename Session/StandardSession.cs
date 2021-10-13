using System.Collections.Generic;
using Godot;

public class StandardSession : Session
{
    const float prospitRadius = 50f;
    const float playerRadius = 100f;
    const float ringRadius = 190f;
    const float derseRadius = 230f;

    RandomNumberGenerator random = new RandomNumberGenerator();

    int Seed;

    // Store some interesting variables
    Spatial ProspitOrbit;
    Spatial DerseOrbit;
    Spatial PlayerOrbits;

    public StandardSession() {
        int seed = 1000;
        random.Seed = (ulong)seed;
        var p = new List<PlayerInfo>();
        /*p.Add(new PlayerInfo(new Page(), new DoomAspect(), new ClassicalElementAir(), seed));
        p.Add(new PlayerInfo(new Heir(), new SpaceAspect(), new ClassicalElementEarth(), seed));
        p.Add(new PlayerInfo(new Rogue(), new VoidAspect(), new ClassicalElementWater(), seed));
        p.Add(new PlayerInfo(new Witch(), new RageAspect(), new ClassicalElementFire(), seed));*/
        _constructMedium(p);
    }
    public StandardSession(List<PlayerInfo> _playerInfos, int seed) {
        this.Seed = seed;
        random.Seed = (ulong)seed;
        _constructMedium(_playerInfos);
    }

    public override void _Process(float delta)
    {
        if (ProspitOrbit == null || DerseOrbit == null || PlayerOrbits == null) {
            return;
        }

        DerseOrbit.Rotate(Vector3.Up, delta * 0.01f);
        ProspitOrbit.Rotate(Vector3.Up, delta * 0.02f);
        PlayerOrbits.Rotate(Vector3.Up, delta * -0.015f);
        base._Process(delta);
    }

    private void _constructMedium(List<PlayerInfo> _playerInfos) {
        // First set up the basics of a session.
        var Skaia = new Skaia(Seed);
        Skaia.Scale = new Vector3(30, 30, 30);

        var Prospit = new Prospit();
        ProspitOrbit = new Spatial();
        
        var Derse = new Derse();
        DerseOrbit = new Spatial();

        // Prospit is our next planet out, put this in orbit of skaia.
        Prospit.Translate(Vector3.Left * Skaia.Scale * prospitRadius);
        Prospit.Scale = new Vector3(10,10,10);
        ProspitOrbit.AddChild(Prospit);

        float step = 360.0f / _playerInfos.Count;
        float stepVal = 0f;

        PlayerOrbits = new Spatial();
        
        // Add the player's planets
        foreach (PlayerInfo p in _playerInfos) {
            var stepRadians = (stepVal / 180) * Mathf.Pi;
            var planet = new PlayerPlanet(Seed, p);
            PlayerOrbits.AddChild(planet);

            planet.Translate(Vector3.Left.Rotated(Vector3.Up, stepRadians) * Skaia.Scale * playerRadius);
            planet.Scale = new Vector3(15, 15, 15);

            stepVal += step;
        }

        // Add the outer ring
        // 500
        var OuterRing = _generateOuterRing(10, Skaia.Scale);

        // Then finally derse outside the outerring
        Derse.Translate(Vector3.Left * Skaia.Scale * derseRadius);
        Derse.Scale = new Vector3(10,10,10);
        DerseOrbit.AddChild(Derse);

        AddChild(PlayerOrbits);
        AddChild(Skaia);
        AddChild(ProspitOrbit);
        AddChild(OuterRing);
        AddChild(DerseOrbit);
    }

    private Node _generateOuterRing(int count, Vector3 skaiaScale) {
        // First create an empty spatial entity
        // Which will act as our 'middle'
        var orbit = new Spatial();

        // Pass the generator into the asteroid constructor to stop us
        // creating hundreds of identical ones.
        OpenSimplexNoise noiseGenerator = new OpenSimplexNoise();
        noiseGenerator.Seed = Seed;
        noiseGenerator.Octaves = 2;
        noiseGenerator.Period = 2f;
        noiseGenerator.Persistence = 0.8f;

        // The ectobiology lab is always added.
        var ectoLab = new EctobiologyLab(noiseGenerator);
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
            var randScale = this.random.RandfRange(1.5f, 5f);
            var randHeight = this.random.RandfRange(-75,75);
            var stepRadians = Mathf.Deg2Rad(stepVal);
            var roid = new EmptyAsteroid(noiseGenerator); // increment the seed value, otherwise all the asteroids (obviously) end up looking the same.

            orbit.AddChild(roid);
            roid.Translate(Vector3.Left.Rotated(Vector3.Up, stepRadians) * skaiaScale * randRingRadius);
            roid.Translate(new Vector3(0, randHeight, 0));
            roid.Scale = Vector3.One * randScale;

            stepVal += step;
        }
        return orbit;
    }
}