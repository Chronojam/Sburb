using System.Collections.Generic;

public class TimeAspect : PlayerAspect {
    public override string Name => "Time";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymClockwork(),
        new PlayerAspectSynonymSeasons(),
    };
};
public class PlayerAspectSynonymClockwork : PlayerAspectSynonym {
    public override string Name => "Clockwork";
}
public class PlayerAspectSynonymSeasons : PlayerAspectSynonym {
    public override string Name => "Seasons";
}
