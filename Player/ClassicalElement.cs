using System.Collections.Generic;

public abstract class ClassicalElement {
    public virtual string Name { get; }
    public virtual ClassicalElement Opposite { get; }
    public virtual List<ClassicalElementSynonym> Synonyms { get;}
}

public abstract class ClassicalElementSynonym {
    public virtual string Name { get; }
}
