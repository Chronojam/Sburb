using Godot.Collections;
using Godot;
using System;

public class ClassicalElementAir : ClassicalElement {
    public override string Name => "Air";
    public override ClassicalElement Opposite => new ClassicalElementEarth();

    public override Array<ClassicalElementSynonym> Synonyms => new Array<ClassicalElementSynonym>() {
                new ClassicalElementSynonymWind(),
                new ClassicalElementSynonymZephyrs(),
                new ClassicalElementSynonymBreezes(),
                new ClassicalElementSynonymCurrents(),
                new ClassicalElementSynonymDrafts(),
            };
}
public class ClassicalElementSynonymWind : ClassicalElementSynonym {
    public override string Name => "Wind";
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}

public class ClassicalElementSynonymZephyrs : ClassicalElementSynonym {
    public override string Name => "Zephyrs";

    private AudioStream windSound;
    private RandomNumberGenerator randomNumberGenerator;

    private int DebugCounter = 0;
    public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        // Load in our sound effects for use later
        windSound = ResourceLoader.Load<AudioStream>("res://Assets/Sounds/Wind-Light_Placeholder.wav");
        randomNumberGenerator = r;
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        return;
        // one in every 100 ish..?
        if (randomNumberGenerator.Randf() < 0.99) {
            return;
        }
        // Lets start here
        var player = new AudioStreamPlayer3D();
        player.Stream = windSound;
        player.Autoplay = true;
        player.MaxDistance = 300f; // Might be really fucking noisy

        // Reposition our music player
        var t = player.Transform;
        t.origin = position;
        player.Transform = t;

        // Add it to the scene.
        parent.AddChild(player);

        DebugCounter++;

        Console.WriteLine($"Generated: {DebugCounter} instances");
    }
}

public class ClassicalElementSynonymBreezes : ClassicalElementSynonym {
    public override string Name => "Breezes";
        public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}

public class ClassicalElementSynonymCurrents : ClassicalElementSynonym {
    public override string Name => "Currents";
        public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}

public class ClassicalElementSynonymDrafts : ClassicalElementSynonym {
    public override string Name => "Drafts";
        public override void ConstructionMod(RandomNumberGenerator r, OpenSimplexNoise n)
    {
        
    }
    public override void QuadMod(Quad parent, Vector3 position, Point iteration)
    {
        
    }
}