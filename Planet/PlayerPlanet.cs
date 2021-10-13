
using Godot;

public class PlayerPlanet : Planet {
    RandomNumberGenerator randomNumberGenerator;
    public PlayerPlanet(int seed, PlayerInfo playerInfo) {
        this.randomNumberGenerator = new RandomNumberGenerator();
        this.randomNumberGenerator.Seed = (ulong)seed;

        var first = playerInfo.SelectedElementSynonym.Name;
        var second = playerInfo.SelectedAspectSynonym.Name;

        this.Name = $"Land of {first} and {second}";
    }
};


