using System.Reflection;

// CardLikeness, How alike is this card to 
// any of the base objects?
public struct CardLikeness {
    // Base Weapon Types
    int Blade;
    int Hammer;
    int Needle;
    int Rifle;
    int Spoon;
    int Fork;
    int Pistol;
    int Broom;
    int Lance;
    int Club;
    int Cane;
    int Wand;
    int Bow;

    // Other stuff
    int Paper;

    // Has functionality of both components
    public CardLikeness AndCombine(CardLikeness other) {
        CardLikeness ret = new CardLikeness();
        foreach( FieldInfo f in GetType().GetFields() ) {
            // Get our own first
            int own = (int)f.GetValue(null);

            // Then the combining card
            int their = (int)f.GetValue(other);

            // Only set if we've both got a value for it (AND)
            if (own != 0 && their != 0) {
                f.SetValue(ret, own + their);
            }
        }

        return ret;
    }
    
    // Has functionality of ourself, but the apperance of 
    // other.
    public CardLikeness OrCombine(CardLikeness other) {
        CardLikeness ret = new CardLikeness();
        foreach( FieldInfo f in GetType().GetFields() ) {
            // Get our own first
            int own = (int)f.GetValue(null);

            // Then the combining card
            int their = (int)f.GetValue(other);

            // Always set to the combined value, even if 0 (OR)
            f.SetValue(ret, own + their);
        }

        return ret;
    }
}

public class PunchCard
{
    public CardLikeness Likeness { get; private set; }

    public PunchCard(CardLikeness l) {
        this.Likeness = l;
    }
}
