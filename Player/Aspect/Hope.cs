using System.Collections.Generic;

public class HopeAspect : PlayerAspect {
    public override string Name => "Hope";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymDreams(), // This is really hard as its so abstract.
    };
};

public class PlayerAspectSynonymDreams : PlayerAspectSynonym {
    public override string Name => "Dreams";
}