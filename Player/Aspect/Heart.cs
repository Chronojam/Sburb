using Godot.Collections;
using Godot;

public class HeartAspect : PlayerAspect {
    public override string Name => "Heart";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymSouls(), // This is really hard as its so abstract.
    };
};

public class PlayerAspectSynonymSouls : PlayerAspectSynonym {
    public override string Name => "Souls";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}