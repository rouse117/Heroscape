using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// temp namespace
namespace Com.NotHeroscape.Launcher
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
    #region Private Serializable Fields

    // Max number of players per room, new room will be created when full
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined and a new room will be created.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    #endregion

    #region Private Fields

    // leave at 1 until major changes to live project
    string gameVersion = "1";

    #endregion

    #region MonoBehaviour Callbacks

    void Awake()
    {
        // have players load same level/scene
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    void Start()
    {

    }

    #endregion

    #region Public Methods

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            // attempt joining a room
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }

    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("NotHeroscape: OnConnectedToMaster was called by PUN");

        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("NotHeroscape: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    #endregion

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("NotHeroscape: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
    
        // we failed to join random room, create rooom
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("NotHeroscape: OnJoinedRoom() called by PUN. This client is in a room.");
    }


    }
}