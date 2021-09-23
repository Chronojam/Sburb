public abstract class PlayerClass {
    public virtual string Name { get; }
};

public class Bard : PlayerClass {
    public override string Name { 
        get {
            return "Bard";
        } 
    }
};
public class Prince : PlayerClass {
    public override string Name { 
        get {
            return "Prince";
        } 
    }
};
public class Heir : PlayerClass {
    public override string Name { 
        get {
            return "Heir";
        } 
    }
};
public class Page : PlayerClass {
    public override string Name { 
        get {
            return "Page";
        } 
    }
};
public class Seer : PlayerClass {
    public override string Name { 
        get {
            return "Seer";
        } 
    }
};
public class Maid : PlayerClass {
    public override string Name { 
        get {
            return "Maid";
        } 
    }
};
public class Sylph : PlayerClass {
    public override string Name { 
        get {
            return "Sylph";
        } 
    }
};
public class Knight : PlayerClass {
    public override string Name { 
        get {
            return "Knight";
        } 
    }
};
public class Witch : PlayerClass {
    public override string Name { 
        get {
            return "Witch";
        } 
    }
};
public class Mage : PlayerClass {
    public override string Name { 
        get {
            return "Mage";
        } 
    }
};