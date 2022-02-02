using Godot.Collections;
using Godot;

public class LifeAspect : PlayerAspect {
    public override string Name => "Life";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymLife(),
        new PlayerAspectSynonymJungles(),
    };
};

public class PlayerAspectSynonymLife : PlayerAspectSynonym {
    public override string Name => "Life";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
    
}
public class PlayerAspectSynonymJungles : PlayerAspectSynonym {
    public override string Name => "Jungles";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}