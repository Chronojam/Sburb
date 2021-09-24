public abstract class PlayerClass {
    public virtual string Name { get; }
};

public class Bard : PlayerClass {
    public override string Name => "Bard";
};
public class Prince : PlayerClass {
    public override string Name => "Prince";
};
public class Heir : PlayerClass {
    public override string Name => "Heir";
};
public class Page : PlayerClass {
    public override string Name => "Page";
};
public class Seer : PlayerClass {
    public override string Name => "Seer";
};
public class Maid : PlayerClass {
    public override string Name => "Maid";
};
public class Thief : PlayerClass {
    public override string Name => "Thief";
};
public class Rogue : PlayerClass {
    public override string Name => "Rogue";
};
public class Sylph : PlayerClass {
    public override string Name => "Sylph";
};
public class Knight : PlayerClass {
    public override string Name => "Knight";
};
public class Witch : PlayerClass {
    public override string Name => "Witch";
};
public class Mage : PlayerClass {
    public override string Name => "Mage";
};