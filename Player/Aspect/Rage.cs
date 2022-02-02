using Godot;
using Godot.Collections;
public class RageAspect : PlayerAspect {
    public override string Name => "Rage";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymWrath(),
        new PlayerAspectSynonymMadness(),
    };
};

public class PlayerAspectSynonymWrath : PlayerAspectSynonym {
    public override string Name => "Wrath";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}

public class PlayerAspectSynonymMadness : PlayerAspectSynonym {
    public override string Name => "Madness";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}