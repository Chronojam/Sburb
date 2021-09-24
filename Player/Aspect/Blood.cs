using System.Collections.Generic;

public class BloodAspect : PlayerAspect {
    public override string Name => "Blood";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymBlood(),
        new PlayerAspectSynonymPlasma(),
        new PlayerAspectSynonymClots(),
    };
};

public class PlayerAspectSynonymBlood : PlayerAspectSynonym {
    public override string Name => "Blood";
}
public class PlayerAspectSynonymPlasma : PlayerAspectSynonym {
    public override string Name => "Plasma";
}
public class PlayerAspectSynonymClots : PlayerAspectSynonym {
    public override string Name => "Clots";
}