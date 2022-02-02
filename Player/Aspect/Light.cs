using Godot;
using Godot.Collections;

public class LightAspect : PlayerAspect {
    public override string Name => "Light";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymBlood(),
        new PlayerAspectSynonymPlasma(),
        new PlayerAspectSynonymClots(),
    };
};

public class PlayerAspectSynonymLight : PlayerAspectSynonym {
    public override string Name => "Light";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymBeacons : PlayerAspectSynonym {
    public override string Name => "Beacons";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymBrilliance : PlayerAspectSynonym {
    public override string Name => "Brilliance";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}