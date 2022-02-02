using Godot.Collections;
using Godot;

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
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymBonfires : ClassicalElementSynonym {
    public override string Name => "Bonfires";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymHeat : ClassicalElementSynonym {
    public override string Name => "Heat";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymCoals : ClassicalElementSynonym {
    public override string Name => "Coals";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymFlames : ClassicalElementSynonym {
    public override string Name => "Flames";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymIncandescence : ClassicalElementSynonym {
    public override string Name => "Incandescence";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}



