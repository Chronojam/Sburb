using System.Collections.Generic;

public class MindAspect : PlayerAspect {
    public override string Name => "Mind";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymThought(),
        new PlayerAspectSynonymBrains(),
        new PlayerAspectSynonymLucidity(),
    };
};

// Neuron shaped structures
public class PlayerAspectSynonymThought : PlayerAspectSynonym {
    public override string Name => "Thought";
}
public class PlayerAspectSynonymBrains : PlayerAspectSynonym {
    public override string Name => "Brains";
}
public class PlayerAspectSynonymLucidity : PlayerAspectSynonym {
    public override string Name => "Lucidity";
} 
