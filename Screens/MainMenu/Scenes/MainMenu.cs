using Godot;
using System;
public class MainMenu : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode("Splitter/Host").Connect("pressed", this, "HostGame");
        GetNode("Splitter/Join").Connect("pressed", this, "JoinGame");
    }

    public void HostGame() {
        // Create a new lobby scene, start a listener.


        Lobby lobby = GetNode<ScreenManager>("/root/Root/Screens").
            LoadScreen<Lobby>("res://Screens/Lobby/Scenes/Lobby.tscn");

        var peer = new NetworkedMultiplayerENet();
        peer.CreateServer(3030, 8);
        GetTree().NetworkPeer = peer;

        // Add the host player, as 'player_connected' never gets called on host 
        // for their own connection
        var json = JSON.Print(new Godot.Collections.Dictionary<int, PlayerInfo>() {
            new System.Collections.Generic.KeyValuePair<int, PlayerInfo>(1, new PlayerInfo(
            new Bard(),
            new BloodAspect(),
            new ClassicalElementAir()
        ))});

        Console.WriteLine(json);

        //lobby.SetPlayers(json);
    }
    private void JoinGame() {
        GetNode<ScreenManager>("/root/Root/Screens").LoadScreen("res://Screens/Lobby/Scenes/Lobby.tscn");

        // Create a new lobby scene, but join a determined listener
        var peer = new NetworkedMultiplayerENet();
        peer.CreateClient("127.0.0.1", 3030);
        GetTree().NetworkPeer = peer;

    }
}
