using System.Collections.Generic;

public class ClassicalElementWater : ClassicalElement {
    public override string Name => "Water";
    public override ClassicalElement Opposite => new ClassicalElementFire();

    public override List<ClassicalElementSynonym> Synonyms => new List<ClassicalElementSynonym>() {
                new ClassicalElementSynonymRain(),
                new ClassicalElementSynonymFloods(),
                new ClassicalElementSynonymMist(),
                new ClassicalElementSynonymStreams(),
                new ClassicalElementSynonymCondensation(),
            };
}
public class ClassicalElementSynonymRain : ClassicalElementSynonym {
    public override string Name => "Rain";
}
public class ClassicalElementSynonymFloods : ClassicalElementSynonym {
    public override string Name => "Floods";
}
public class ClassicalElementSynonymMist : ClassicalElementSynonym {
    public override string Name => "Mist";
}
public class ClassicalElementSynonymStreams : ClassicalElementSynonym {
    public override string Name => "Streams";
}
public class ClassicalElementSynonymCondensation : ClassicalElementSynonym {
    public override string Name => "Condensation";
}