using Godot.Collections;
using Godot;

public class HopeAspect : PlayerAspect {
    public override string Name => "Hope";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymDreams(), // This is really hard as its so abstract.
    };
};

public class PlayerAspectSynonymDreams : PlayerAspectSynonym {
    public override string Name => "Dreams";

    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}