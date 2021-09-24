using System.Collections.Generic;

public class ClassicalElementEarth : ClassicalElement {
    public override string Name => "Earth"; 
    public override ClassicalElement Opposite => new ClassicalElementAir();
    public override List<ClassicalElementSynonym> Synonyms => new List<ClassicalElementSynonym>() {
                new ClassicalElementSynonymDust(),
                new ClassicalElementSynonymSand(),
                new ClassicalElementSynonymClay(),
                new ClassicalElementSynonymLoam(),
                new ClassicalElementSynonymBoulders(),
                new ClassicalElementSynonymGravel(),
            };
        
    
}public class ClassicalElementSynonymDust : ClassicalElementSynonym {
    public override string Name => "Dust";
}
public class ClassicalElementSynonymSand : ClassicalElementSynonym {
    public override string Name => "Sand";
}
public class ClassicalElementSynonymClay : ClassicalElementSynonym {
    public override string Name => "Clay";
}
public class ClassicalElementSynonymLoam : ClassicalElementSynonym {
    public override string Name => "Loam";
}
public class ClassicalElementSynonymBoulders : ClassicalElementSynonym {
    public override string Name => "Boulders";
}
public class ClassicalElementSynonymGravel : ClassicalElementSynonym {
    public override string Name => "Gravel";
}
