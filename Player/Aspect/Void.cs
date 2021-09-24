using System.Collections.Generic;

public class VoidAspect : PlayerAspect {
    public override string Name => "Void";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymSilence(),
        new PlayerAspectSynonymPlasma(),
        new PlayerAspectSynonymClots(),
    };
};

public class PlayerAspectSynonymSilence : PlayerAspectSynonym {
    public override string Name => "Silence";
}
public class PlayerAspectSynonymSpace : PlayerAspectSynonym {
    public override string Name => "Space";
}
public class PlayerAspectSynonymChasms : PlayerAspectSynonym {
    public override string Name => "Chasms";
}