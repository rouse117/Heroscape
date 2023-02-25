using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PUN_Manager_Script : MonoBehaviourPunCallbacks
{
    private const string ROOM_NAME = "myRoom";
    private const int MAX_PLAYERS = 2;

    string gameVersion = "1";

    private void Start() {
        Connect();
    }

    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, new RoomOptions { MaxPlayers = MAX_PLAYERS }, TypedLobby.Default);
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, new RoomOptions { MaxPlayers = MAX_PLAYERS }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        int playerValue = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerValue == 1)
        {
            Debug.Log("You are Player 1");
        }
        else if (playerValue == 2)
        {
            Debug.Log("You are Player 2");
        }
    }
}



