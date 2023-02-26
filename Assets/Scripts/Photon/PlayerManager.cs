using UnityEngine;

using Photon.Pun;

using System.Collections;

namespace Com.NotHeroscape
{

    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            
            DontDestroyOnLoad(this.gameObject);
        }
    }

}