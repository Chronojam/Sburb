using Godot;
using Godot.Collections;

public class VoidAspect : PlayerAspect {
    public override string Name => "Void";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymSilence(),
        new PlayerAspectSynonymPlasma(),
        new PlayerAspectSynonymClots(),
    };
};

public class PlayerAspectSynonymSilence : PlayerAspectSynonym {
    public override string Name => "Silence";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymSpace : PlayerAspectSynonym {
    public override string Name => "Space";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymChasms : PlayerAspectSynonym {
    public override string Name => "Chasms";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}