using Godot;
using Godot.Collections;
// PlayerInfo is just the information about a given player.
// represented as a node so we can use godot's networking features.
public class PlayerInfo : Object {
    public PlayerClass Class;
    public PlayerAspect Aspect;
    public ClassicalElement Element;


    // Generated from the other fields.
    public ClassicalElementSynonym SelectedElementSynonym;
    public PlayerAspectSynonym SelectedAspectSynonym;

    public PlayerInfo(PlayerClass c, PlayerAspect a, ClassicalElement e) {
        this.Class = c;
        this.Aspect = a;
        this.Element = e;
    }

    public Dictionary<string, object> ToSerializable() {
        return new Dictionary<string, object>() {
            new System.Collections.Generic.KeyValuePair<string, object>("PlayerClass", this.Class.ToSerializable),
            new System.Collections.Generic.KeyValuePair<string, object>("PlayerAspect", this.Aspect.ToSerializable),
            new System.Collections.Generic.KeyValuePair<string, object>("ClassicalElement", this.Element.ToSerializable),
        };
    }

    public static PlayerInfo FromDictionary(Dictionary<string, object> dict) {
        PlayerClass c = PlayerClass.FromString((string)dict["PlayerClass"]);
        PlayerAspect a = PlayerAspect.FromString((string)dict["PlayerAspect"]);
        ClassicalElement e = ClassicalElement.FromString((string)dict["ClassicalElement"]);
        return new PlayerInfo(c, a, e);
    }

    public void Init(int seed) {
        // Select element and aspect words.
        // Store these in the playerinfo for easy syncing.
        RandomNumberGenerator r = new RandomNumberGenerator();
        r.Seed = (ulong)seed;

        var index = r.RandiRange(0, Element.Synonyms.Count - 1);
        this.SelectedElementSynonym = Element.Synonyms[index];

        index = r.RandiRange(0, Aspect.Synonyms.Count - 1);
        this.SelectedAspectSynonym = Aspect.Synonyms[index];
    }

    string Title { 
        get {
            return $"${Class.Name} of ${Aspect.Name}";
        }
    }
}