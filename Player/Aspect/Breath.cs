using System.Collections.Generic;

public class BreathAspect : PlayerAspect {
    public override string Name => "Breath";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
    }; // This is really hard as its so abstract.
};
