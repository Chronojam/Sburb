using System.Collections.Generic;

public class DoomAspect : PlayerAspect {
    public override string Name => "Doom";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymDeath(),
        new PlayerAspectSynonymDestiny(),
        new PlayerAspectSynonymJudgement(),
    };
};

public class PlayerAspectSynonymDeath : PlayerAspectSynonym {
    public override string Name => "Death";
}
public class PlayerAspectSynonymDestiny : PlayerAspectSynonym {
    public override string Name => "Destiny";
}
public class PlayerAspectSynonymJudgement : PlayerAspectSynonym {
    public override string Name => "Judgement";
}