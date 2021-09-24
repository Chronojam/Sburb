using System.Collections.Generic;

public class LightAspect : PlayerAspect {
    public override string Name => "Light";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymBlood(),
        new PlayerAspectSynonymPlasma(),
        new PlayerAspectSynonymClots(),
    };
};

public class PlayerAspectSynonymLight : PlayerAspectSynonym {
    public override string Name => "Light";
}
public class PlayerAspectSynonymBeacons : PlayerAspectSynonym {
    public override string Name => "Beacons";
}
public class PlayerAspectSynonymBrilliance : PlayerAspectSynonym {
    public override string Name => "Brilliance";
}