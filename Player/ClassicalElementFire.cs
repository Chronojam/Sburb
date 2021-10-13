using Godot.Collections;

public class ClassicalElementFire : ClassicalElement {
    public override string Name => "Fire"; 
    public override ClassicalElement Opposite => new ClassicalElementWater();
    public override Array<ClassicalElementSynonym> Synonyms => new Array<ClassicalElementSynonym>() {
                new ClassicalElementSynonymBlazes(),
                new ClassicalElementSynonymBonfires(),
                new ClassicalElementSynonymHeat(),
                new ClassicalElementSynonymCoals(),
                new ClassicalElementSynonymFlames(),
                new ClassicalElementSynonymIncandescence(),
            };
        
    
}public class ClassicalElementSynonymBlazes : ClassicalElementSynonym {
    public override string Name => "Blazes";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymBonfires : ClassicalElementSynonym {
    public override string Name => "Bonfires";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymHeat : ClassicalElementSynonym {
    public override string Name => "Heat";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymCoals : ClassicalElementSynonym {
    public override string Name => "Coals";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymFlames : ClassicalElementSynonym {
    public override string Name => "Flames";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymIncandescence : ClassicalElementSynonym {
    public override string Name => "Incandescence";
    public override void Mod(Planet p)
    {
        
    }
}



