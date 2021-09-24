using System.Collections.Generic;

public class ClassicalElementAir : ClassicalElement {
    public override string Name => "Air";
    public override ClassicalElement Opposite => new ClassicalElementEarth();

    public override List<ClassicalElementSynonym> Synonyms => new List<ClassicalElementSynonym>() {
                new ClassicalElementSynonymWind(),
                new ClassicalElementSynonymZephyrs(),
                new ClassicalElementSynonymBreezes(),
                new ClassicalElementSynonymCurrents(),
                new ClassicalElementSynonymDrafts(),
            };
}
public class ClassicalElementSynonymWind : ClassicalElementSynonym {
    public override string Name => "Wind";
}

public class ClassicalElementSynonymZephyrs : ClassicalElementSynonym {
    public override string Name => "Zephyrs";
}

public class ClassicalElementSynonymBreezes : ClassicalElementSynonym {
    public override string Name => "Breezes";
}

public class ClassicalElementSynonymCurrents : ClassicalElementSynonym {
    public override string Name => "Currents";
}

public class ClassicalElementSynonymDrafts : ClassicalElementSynonym {
    public override string Name => "Drafts";
}