using Godot;
using Godot.Collections;
public abstract class PlayerAspect : Object {
    public virtual string Name { get; }
    public virtual Array<PlayerAspectSynonym> Synonyms { get;}

    public virtual string ToSerializable => this.Name;

    public static PlayerAspect FromString(string d) {
        switch (d) {
            case "Blood":
                return new BloodAspect();
            case "Breath":
                return new BreathAspect();
            case "Doom":
                return new DoomAspect();
            case "Heart":
                return new HeartAspect();
            case "Hope":
                return new HopeAspect();
            case "Life":
                return new LifeAspect();
            case "Light":
                return new LightAspect();
            case "Mind":
                return new MindAspect();
            case "Rage":
                return new RageAspect();
            case "Space":
                return new SpaceAspect();
            case "Time":
                return new TimeAspect();
            case "Void":
                return new VoidAspect();
            default:
                throw new System.Exception("Unknown Aspect: " + d);
        }        
    }
};

public abstract class PlayerAspectSynonym : Object, IPlanetaryModifier {
    public virtual string Name { get; }

    private RandomNumberGenerator randomNumberGenerator;
    private OpenSimplexNoise noiseGenerator;

    public virtual void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n) {
        randomNumberGenerator = r;
        noiseGenerator = n;
    }

    public abstract void QuadMod(Quad parent, Vector3 position, Point iteration);
}