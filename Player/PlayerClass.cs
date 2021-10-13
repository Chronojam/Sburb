using Godot;
public abstract class PlayerClass : Object {
    public virtual string Name { get; }
    public virtual string ToSerializable => this.Name;
    public static PlayerClass FromString(string d) {
        switch (d) {
            case "Bard":
                return new Bard();
            case "Prince":
                return new Prince();
            case "Heir":
                return new Heir();
            case "Page":
                return new Page();
            case "Seer":
                return new Seer();
            case "Maid":
                return new Maid();
            case "Thief":
                return new Thief();
            case "Rogue":
                return new Rogue();
            case "Sylph":
                return new Sylph();
            case "Knight":
                return new Knight();
            case "Witch":
                return new Witch();
            default:
                throw new System.Exception("Unknown Class: " + d);
        }
    }
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