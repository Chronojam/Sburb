using System.Collections.Generic;

public class LifeAspect : PlayerAspect {
    public override string Name => "Life";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymLife(),
        new PlayerAspectSynonymJungles(),
    };
};

public class PlayerAspectSynonymLife : PlayerAspectSynonym {
    public override string Name => "Life";
}
public class PlayerAspectSynonymJungles : PlayerAspectSynonym {
    public override string Name => "Jungles";
}