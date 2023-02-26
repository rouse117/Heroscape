using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace Com.NotHeroscape
{
        
    public class GameManager : MonoBehaviourPunCallbacks
    {

    // Start is called before the first frame update
    void Start()
    {
        if (unitPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> unitPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
        }
        else
        {
            
            if (PlayerManager.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                PhotonNetwork.Instantiate(this.unitPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Photon Callbacks

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    [Tooltip("The prefab to use for representing the player")]
    public GameObject unitPrefab;
    
    
    #endregion
}


}