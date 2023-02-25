using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.SceneManagement;

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

    [Tooltip("The PUN room being joined.")]
    [SerializeField]
    private string roomName = "myRoom";

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;

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
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    #endregion

    #region Public Methods

    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        
        if (PhotonNetwork.IsConnected)
        {
                // attempt joining a room
               PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, TypedLobby.Default); ;
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }

    }

    public void LocalPlay()
    {
        SceneManager.LoadScene(1);
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
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);

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
        SceneManager.LoadScene(1);
    }


    }
}