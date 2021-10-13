using Godot.Collections;

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
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymFloods : ClassicalElementSynonym {
    public override string Name => "Floods";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymMist : ClassicalElementSynonym {
    public override string Name => "Mist";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymStreams : ClassicalElementSynonym {
    public override string Name => "Streams";
    public override void Mod(Planet p)
    {
        
    }
}
public class ClassicalElementSynonymCondensation : ClassicalElementSynonym {
    public override string Name => "Condensation";
    public override void Mod(Planet p)
    {
        
    }
}