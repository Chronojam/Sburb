
using Godot;
using System.Collections.Generic;

class Lobby : Node {

    private Dictionary<int, PlayerInfo> _players;
    
    [Puppet]
    Dictionary<int, PlayerInfo> Players { 
        get {
            return _players;
        } 
        set {
            _players = value;
            updateUI();
        }
    }

    public override void _Ready()
    {
        GetTree().Connect("network_peer_connected", this, "PlayerConnected");
        GetTree().Connect("network_peer_disconnected", this, "PlayerDisconnected");
        GetTree().Connect("connected_to_server", this, "ConnectedOk");
        GetTree().Connect("connection_failed", this, "ConnectionFailed");
        GetTree().Connect("server_disconnected", this, "ServerDisconnected");
        base._Ready();
    }

    public void PlayerConnected(int id) {
        // Called on both client and server when someone connects
        // If we're a client, do nothing and wait for an update from the server.
        if (GetTree().GetNetworkUniqueId() != 1) {
            return;
        }

        Players.Add(id, new PlayerInfo());
        // Call this manually here, as we're only adding on the server
        // side, rather than explictly setting the list.
        updateUI();

        // Inform clients of new state of _players
        Rset("Players", Players);
    }
    public void PlayerDisconnected(int id) {
        // Called on both client and server when someone disconnects
        if (GetTree().GetNetworkUniqueId() != 1) {
            return;
        }

        Players.Remove(id);
        updateUI();
        Rset("Players", Players);
    }

    [Master]
    public void PlayerUpdated(PlayerInfo p) {
        var callerId = GetTree().GetRpcSenderId();

        // For now, just accept whatever someone sends us
        // TODO validate playerstate here.
        Players[callerId] = p;
        updateUI();

        Rset("Players", Players);
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
    }
}