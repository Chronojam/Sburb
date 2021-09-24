using System.Collections.Generic;

public class ClassicalElementFire : ClassicalElement {
    public override string Name => "Fire"; 
    public override ClassicalElement Opposite => new ClassicalElementWater();
    public override List<ClassicalElementSynonym> Synonyms => new List<ClassicalElementSynonym>() {
                new ClassicalElementSynonymBlazes(),
                new ClassicalElementSynonymBonfires(),
                new ClassicalElementSynonymHeat(),
                new ClassicalElementSynonymCoals(),
                new ClassicalElementSynonymFlames(),
                new ClassicalElementSynonymIncandescence(),
            };
        
    
}public class ClassicalElementSynonymBlazes : ClassicalElementSynonym {
    public override string Name => "Blazes";
}
public class ClassicalElementSynonymBonfires : ClassicalElementSynonym {
    public override string Name => "Bonfires";
}
public class ClassicalElementSynonymHeat : ClassicalElementSynonym {
    public override string Name => "Heat";
}
public class ClassicalElementSynonymCoals : ClassicalElementSynonym {
    public override string Name => "Coals";
}
public class ClassicalElementSynonymFlames : ClassicalElementSynonym {
    public override string Name => "Flames";
}
public class ClassicalElementSynonymIncandescence : ClassicalElementSynonym {
    public override string Name => "Incandescence";
}



