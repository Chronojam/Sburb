
using Godot;

public class PlayerPlanet : Planet {
    RandomNumberGenerator randomNumberGenerator;
    public PlayerPlanet(RandomNumberGenerator rand, PlayerInfo playerInfo) {
        this.randomNumberGenerator = rand;

        var first = elementWord(playerInfo.Element);
        var second = aspectWord(playerInfo.Aspect);
        this.Name = $"Land of {first} and {second}";
    }

    private string elementWord(ClassicalElement e) {
        // Pick one of the synonyms at random.
        var index = randomNumberGenerator.RandiRange(0, e.Synonyms.Count - 1);
        return e.Synonyms[index].Name;
    }

    private string aspectWord(PlayerAspect a) {
        var index = randomNumberGenerator.RandiRange(0, a.Synonyms.Count - 1);
        return a.Synonyms[index].Name;
    }
};


