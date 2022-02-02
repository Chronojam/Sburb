using Godot;
using Godot.Collections;

public class DoomAspect : PlayerAspect {
    public override string Name => "Doom";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymDeath(),
        new PlayerAspectSynonymDestiny(),
        new PlayerAspectSynonymJudgement(),
    };
};

public class PlayerAspectSynonymDeath : PlayerAspectSynonym {
    public override string Name => "Death";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymDestiny : PlayerAspectSynonym {
    public override string Name => "Destiny";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        // Lets just do a quick test, in our example world, seed: 1000
        // the player's land is always zepyhr's and destiny
        // So lets start by implementing 'Destiny' whatever the hell thats supposed to mean.
        // How does a land called destiny alter its appearence

        // Just consulting the experts.
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymJudgement : PlayerAspectSynonym {
    public override string Name => "Judgement";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}