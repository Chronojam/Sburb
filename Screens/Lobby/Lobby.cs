
using Godot;
using Godot.Collections;


class PlayerDict : Dictionary<int, PlayerInfo> {
    public Dictionary<int, Dictionary<string, object>> ToSerializable() {
        var ret = new Dictionary<int, Dictionary<string, object>>();
        foreach (var item in this) {
            ret.Add(item.Key, item.Value.ToSerializable());
        }
        return ret;
    }

    public static PlayerDict FromDictionary(Dictionary<int, Dictionary<string, object>> dict) {
        var ret = new PlayerDict();
        foreach(var item in dict) {
            ret.Add(item.Key, PlayerInfo.FromDictionary(item.Value));
        }
        return ret;
    }
}

class Lobby : Node {

    private PlayerDict Players;

    private int _seed = 1000;
    

    [Puppet]
    public void SetPlayers(Dictionary<int, Dictionary<string, object>> infos) {
        Players = PlayerDict.FromDictionary(infos);
        updateUI();
    }

    public override void _Ready()
    {
        GetTree().Connect("network_peer_connected", this, "PlayerConnected");
        GetTree().Connect("network_peer_disconnected", this, "PlayerDisconnected");
        GetTree().Connect("connected_to_server", this, "ConnectedOk");
        GetTree().Connect("connection_failed", this, "ConnectionFailed");
        GetTree().Connect("server_disconnected", this, "ServerDisconnected");

        // Hook up some ui elements.
        GetNode("Splitter/RightPanel/StartGameButton").Connect("pressed", this, "StartGameButton");

        Players = new PlayerDict();
        base._Ready();
    }

    public void PlayerConnected(int id) {
        // Called on both client and server when someone connects
        // If we're a client, do nothing and wait for an update from the server.
        if (GetTree().GetNetworkUniqueId() != 1) {
            return;
        }

        // By default
        Players.Add(id, new PlayerInfo(
            new Bard(),
            new BloodAspect(),
            new ClassicalElementAir()
        ));
        // Call this manually here, as we're only adding on the server
        // side, rather than explictly setting the list.
        updateUI();

        // Inform clients of new state of Players
        Rpc("SetPlayers", new object[1] { Players.ToSerializable() });
        
    }
    public void PlayerDisconnected(int id) {
        // Called on both client and server when someone disconnects
        if (GetTree().GetNetworkUniqueId() != 1) {
            return;
        }

        Players.Remove(id);
        updateUI();
        Rpc("SetPlayers", new object[1] { Players });
    }

    [Master]
    public void PlayerUpdated(PlayerInfo p) {
        var callerId = GetTree().GetRpcSenderId();

        // For now, just accept whatever someone sends us
        // TODO validate playerstate here.
        Players[callerId] = p;
        updateUI();

        Rpc("SetPlayers", new object[1] { Players });
    }

    public void ConnectedOk() {
        // Only called on clients when we connect ok
    }
    public void ConnectionFailed() {
        // Only called on clients when they are unable to connect to the server
    }
    public void ServerDisconnected() {
        // Only called on client when a server force disconnected us
    }
    private void updateUI() {
        // Updates the associated UI, call me after altering the state.
        var pList = GetNode("Splitter/RightPanel/PlayerListArea/PlayerList");
        var item = ResourceLoader.Load<PackedScene>("res://Screens/Lobby/Scenes/PlayerListItem.tscn");

        // Remove all the existing nonsense
        foreach(Node child in pList.GetChildren()) {
            child.QueueFree();
        }

        foreach( var pair in Players) {
            var thing = item.Instance();
            thing.GetNode<Label>("Bar/Name").Text = "A Player";
            thing.GetNode<Label>("Bar/Aspect").Text = pair.Value.Aspect.Name;

            pList.AddChild(thing);
        }
    }


    // When the host clicks start game
    private void StartGameButton() {
        if (GetTree().GetNetworkUniqueId() != 1) {
            return;
        }
        // Tell all the players to initialize their sessions.
        Rpc("InitializeSession", new object[1]{ _seed });
    }

    [RemoteSync]
    private void InitializeSession(int Seed) {
        // Pause the game
        GetTree().Paused = true;

        foreach (var key in Players) {
            key.Value.Init(Seed);
        }

        // For now, always use standard sessions
        //var session = new StandardSession(p, Seed);
        //session.Name = "Session";
        //GetNode("/").AddChild(session);

        // Tell the server that we're done loading.
        RpcId(1, "DoneLoading");
    }


    private Array<int> LoadedPlayers = new Array<int>();
    [Master]
    private void DoneLoading() {
        LoadedPlayers.Add(GetTree().GetRpcSenderId());

        if (LoadedPlayers.Count == Players.Count) {
            // Everyone's loaded so tell everyone to actually start the game
            Rpc("StartTheGameForReal");
        }
    }

    [RemoteSync]
    private void StartTheGameForReal() {
        GetTree().Paused = false;
        // Delete the lobby
        this.QueueFree();
    }
}