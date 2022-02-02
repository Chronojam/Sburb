using Godot;

public interface IPlanetaryModifier {
    // Allows a modifier to alter a planet during the construction phase
    void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n);

    // Allows a modifier to alter a planet during the 'ready' phase
    // Here we'll take in the LOD of the quad (for model placement),
    // We'll take the random number generator from earlier and store it in this class.
    void QuadMod(Quad parent, Vector3 position, Point iteration);
}