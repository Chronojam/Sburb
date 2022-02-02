using Godot;
using Godot.Collections;
public class TimeAspect : PlayerAspect {
    public override string Name => "Time";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymClockwork(),
        new PlayerAspectSynonymSeasons(),
    };
};
public class PlayerAspectSynonymClockwork : PlayerAspectSynonym {
    public override string Name => "Clockwork";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}

public class PlayerAspectSynonymSand : PlayerAspectSynonym {
    public override string Name => "Sand";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}

public class PlayerAspectSynonymSeasons : PlayerAspectSynonym {
    public override string Name => "Seasons";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}

