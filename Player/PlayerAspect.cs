public abstract class PlayerAspect {
    public virtual string Name { get; }
};

public class SpaceAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Space";
        } 
    }
};
public class TimeAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Time";
        } 
    }
};
public class MindAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Mind";
        } 
    }
};
public class HeartAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Heart";
        } 
    }
};
public class HopeAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Hope";
        } 
    }
};
public class RageAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Rage";
        } 
    }
};
public class BreathAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Breath";
        } 
    }
};
public class BloodAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Blood";
        } 
    }
};
public class LifeAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Life";
        } 
    }
};
public class DoomAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Doom";
        } 
    }
};
public class LightAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Light";
        } 
    }    
};
public class VoidAspect : PlayerAspect {
    public override string Name { 
        get {
            return "Void";
        } 
    }
};

