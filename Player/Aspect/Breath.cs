using Godot.Collections;

public class BreathAspect : PlayerAspect {
    public override string Name => "Breath";
    public override Array<PlayerAspectSynonym> Synonyms => new Array<PlayerAspectSynonym>() {
    }; // This is really hard as its so abstract.
};
