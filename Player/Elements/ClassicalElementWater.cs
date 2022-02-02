using Godot.Collections;
using Godot;

public class ClassicalElementWater : ClassicalElement {
    public override string Name => "Water";
    public override ClassicalElement Opposite => new ClassicalElementFire();

    public override Array<ClassicalElementSynonym> Synonyms => new Array<ClassicalElementSynonym>() {
                new ClassicalElementSynonymRain(),
                new ClassicalElementSynonymFloods(),
                new ClassicalElementSynonymMist(),
                new ClassicalElementSynonymStreams(),
                new ClassicalElementSynonymCondensation(),
            };
}
public class ClassicalElementSynonymRain : ClassicalElementSynonym {
    public override string Name => "Rain";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymFloods : ClassicalElementSynonym {
    public override string Name => "Floods";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymMist : ClassicalElementSynonym {
    public override string Name => "Mist";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymStreams : ClassicalElementSynonym {
    public override string Name => "Streams";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}
public class ClassicalElementSynonymCondensation : ClassicalElementSynonym {
    public override string Name => "Condensation";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}