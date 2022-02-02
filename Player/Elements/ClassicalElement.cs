
using Godot;
using Godot.Collections;
public abstract class ClassicalElement : Object {
    public virtual string Name { get; }
    public virtual ClassicalElement Opposite { get; }
    public virtual Array<ClassicalElementSynonym> Synonyms { get;}

    public virtual string ToSerializable => this.Name;

    public static ClassicalElement FromString(string d) {
        switch (d) {
            case "Air":
                return new ClassicalElementAir();
            case "Earth":
                return new ClassicalElementEarth();
            case "Fire":
                return new ClassicalElementFire();
            case "Water":
                return new ClassicalElementWater();
            default:
                throw new System.Exception("Unknown Classical Element: " + d);
        }        
    }
}

public abstract class ClassicalElementSynonym : Object, IPlanetaryModifier {
    public virtual string Name { get; }

    private RandomNumberGenerator numberGenerator;
    private OpenSimplexNoise noiseGenerator;

    // Allows modification of planet parameters during construction
    public virtual void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n) {
        numberGenerator = r;
        noiseGenerator = n;
    }
    public abstract void QuadMod(Quad parent, Vector3 position, Point iteration);
}
