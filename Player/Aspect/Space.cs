using Godot;
using Godot.Collections;

public class SpaceAspect : PlayerAspect {
    public override string Name => "Space";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
        new PlayerAspectSynonymFrogs()
    };
};
public class PlayerAspectSynonymFrogs : PlayerAspectSynonym {
    public override string Name => "Frogs";

    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
