using Godot.Collections;

public class ClassicalElementAir : ClassicalElement {
    public override string Name => "Air";
    public override ClassicalElement Opposite => new ClassicalElementEarth();

    public override Array<ClassicalElementSynonym> Synonyms => new Array<ClassicalElementSynonym>() {
                new ClassicalElementSynonymWind(),
                new ClassicalElementSynonymZephyrs(),
                new ClassicalElementSynonymBreezes(),
                new ClassicalElementSynonymCurrents(),
                new ClassicalElementSynonymDrafts(),
            };
}
public class ClassicalElementSynonymWind : ClassicalElementSynonym {
    public override string Name => "Wind";
    public override void Mod(Planet p)
    {
        
    }
}

public class ClassicalElementSynonymZephyrs : ClassicalElementSynonym {
    public override string Name => "Zephyrs";
    public override void Mod(Planet p)
    {
        
    }
}

public class ClassicalElementSynonymBreezes : ClassicalElementSynonym {
    public override string Name => "Breezes";
    public override void Mod(Planet p)
    {
        
    }
}

public class ClassicalElementSynonymCurrents : ClassicalElementSynonym {
    public override string Name => "Currents";
    public override void Mod(Planet p)
    {
        
    }
}

public class ClassicalElementSynonymDrafts : ClassicalElementSynonym {
    public override string Name => "Drafts";
    public override void Mod(Planet p)
    {
        
    }
}