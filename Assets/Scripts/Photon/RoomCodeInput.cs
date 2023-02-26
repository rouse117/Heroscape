using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;

namespace Com.NotHeroscape
{
    // force InputField
    // [RequireComponent(typeof(InputField))]
    public class RoomCodeInputField : MonoBehaviour
    {
        #region Private Constants

    const string roomCodePrefKey = "DefaultRoom";

    #endregion

    #region MonoBehaviour CallBacks

    void Start () {

        string defaultCode = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField!=null)
        {
            // player name for project
            if (PlayerPrefs.HasKey(roomCodePrefKey))
            {
                defaultCode = PlayerPrefs.GetString(roomCodePrefKey);
                _inputField.text = defaultCode;
            }
        }

        // name on network
        // PhotonNetwork.NickName =  defaultCode;
    }

    #endregion

    #region Public Methods

    // value change to set name
    public void SetRoomCode(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Room code is null or empty");
            return;
        }
        // PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(roomCodePrefKey,value);
    }

    #endregion
}

}