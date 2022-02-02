using Godot.Collections;
using Godot;

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
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymSand : ClassicalElementSynonym {
    public override string Name => "Sand";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymClay : ClassicalElementSynonym {
    public override string Name => "Clay";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymLoam : ClassicalElementSynonym {
    
    public override string Name => "Loam";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymBoulders : ClassicalElementSynonym {
    public override string Name => "Boulders";

    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymGravel : ClassicalElementSynonym {
    public override string Name => "Gravel";

    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
