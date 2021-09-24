using System.Collections.Generic;

public class SpaceAspect : PlayerAspect {
    public override string Name => "Space";
    public override List<PlayerAspectSynonym> Synonyms => new List<PlayerAspectSynonym>() {
        new PlayerAspectSynonymFrogs() // Its always frogs for space players.
    };
};
public class PlayerAspectSynonymFrogs : PlayerAspectSynonym {
    public override string Name => "Frogs";
}
