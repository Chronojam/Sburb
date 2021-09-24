using System.Collections.Generic;

public abstract class PlayerAspect {
    public virtual string Name { get; }
    public virtual List<PlayerAspectSynonym> Synonyms { get;}
};

public abstract class PlayerAspectSynonym {
    public virtual string Name { get; }
}