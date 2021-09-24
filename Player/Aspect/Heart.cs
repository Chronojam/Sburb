using System.Collections.Generic;

public class HeartAspect : PlayerAspect {
    public override string Name => "Heart";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymSouls(), // This is really hard as its so abstract.
    };
};

public class PlayerAspectSynonymSouls : PlayerAspectSynonym {
    public override string Name => "Souls";
}