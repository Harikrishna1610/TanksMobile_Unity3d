using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class TankColorHook : LobbyHook {

    // Use this for initialization
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        TankColor tank = gamePlayer.GetComponent<TankColor>();
        tank.color = lobby.playerColor;
        
    }
}
