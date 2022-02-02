using Godot;
using Godot.Collections;

public class BloodAspect : PlayerAspect {
    public override string Name => "Blood";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymBlood(),
        new PlayerAspectSynonymPlasma(),
        new PlayerAspectSynonymClots(),
    };
};

public class PlayerAspectSynonymBlood : PlayerAspectSynonym {
    public override string Name => "Blood";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymPlasma : PlayerAspectSynonym {
    public override string Name => "Plasma";
        public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }


    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymClots : PlayerAspectSynonym {
    public override string Name => "Clots";
        public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}