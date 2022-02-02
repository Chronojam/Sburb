using Godot;
using Godot.Collections;

public class MindAspect : PlayerAspect {
    public override string Name => "Mind";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymThought(),
        new PlayerAspectSynonymBrains(),
        new PlayerAspectSynonymLucidity(),
    };
};

// Neuron shaped structures
public class PlayerAspectSynonymThought : PlayerAspectSynonym {
    public override string Name => "Thought";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymBrains : PlayerAspectSynonym {
    public override string Name => "Brains";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class PlayerAspectSynonymLucidity : PlayerAspectSynonym {
    public override string Name => "Lucidity";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }

    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
} 
