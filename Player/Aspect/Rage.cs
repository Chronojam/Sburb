using System.Collections.Generic;

public class RageAspect : PlayerAspect {
    public override string Name => "Rage";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymWrath(),
        new PlayerAspectSynonymMadness(),
    };
};

public class PlayerAspectSynonymWrath : PlayerAspectSynonym {
    public override string Name => "Wrath";
}

public class PlayerAspectSynonymMadness : PlayerAspectSynonym {
    public override string Name => "Madness";
}