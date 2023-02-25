using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Cube_Multiplayer_Script: MonoBehaviourPun
{
    [SerializeField]
    private int serializedValue = 0;

    private int controllingPlayer = 0;

    private PhotonView photonView;

    private void Start()
    {
        // Get the PhotonView component
        photonView = GetComponent<PhotonView>();

        if (PhotonNetwork.IsConnected)
        {
            // If this is a networked game, set the ownership of the object
            photonView.TransferOwnership(serializedValue);
        }
    }

    private void Update()
    {
        if (controllingPlayer == 0)
        {
            // If no one is controlling the object, check if the local player should control it
            if (PhotonNetwork.LocalPlayer.ActorNumber == serializedValue)
            {
                controllingPlayer = serializedValue;
                Debug.Log("Player " + controllingPlayer + " has assumed control of the object");

                // If the local player is controlling the object, set the ownership to the local player
                photonView.RequestOwnership();
            }
        }
        else if (controllingPlayer == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            // If the local player is controlling the object, allow them to move it
            // float moveSpeed = 5f;
            // float horizontalInput = Input.GetAxis("Horizontal");
            // float verticalInput = Input.GetAxis("Vertical");
            // transform.Translate(new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime);

            // If the local player is controlling the object, synchronize the position across clients
            photonView.RPC("SyncPosition", RpcTarget.Others, transform.position);
        }
    }

    [PunRPC]
    private void SyncPosition(Vector3 position)
    {
        // Update the position of the object on other clients
        transform.position = position;
    }
}


