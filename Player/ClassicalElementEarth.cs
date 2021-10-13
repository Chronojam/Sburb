using Godot.Collections;

public class ClassicalElementEarth : ClassicalElement {
    public override string Name => "Earth"; 
    public override ClassicalElement Opposite => new ClassicalElementAir();
    public override Array<ClassicalElementSynonym> Synonyms => new Array<ClassicalElementSynonym>() {
                new ClassicalElementSynonymDust(),
                new ClassicalElementSynonymSand(),
                new ClassicalElementSynonymClay(),
                new ClassicalElementSynonymLoam(),
                new ClassicalElementSynonymBoulders(),
                new ClassicalElementSynonymGravel(),
            };
        
    
}public class ClassicalElementSynonymDust : ClassicalElementSynonym {
    public override string Name => "Dust";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymSand : ClassicalElementSynonym {
    public override string Name => "Sand";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymClay : ClassicalElementSynonym {
    public override string Name => "Clay";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymLoam : ClassicalElementSynonym {
    
    public override string Name => "Loam";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymBoulders : ClassicalElementSynonym {
    public override string Name => "Boulders";

    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymGravel : ClassicalElementSynonym {
    public override string Name => "Gravel";

    public override void Mod(Planet p)
    {
        
    }
}
