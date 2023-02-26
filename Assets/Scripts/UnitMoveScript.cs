using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UnitMoveScript : MonoBehaviour
{
    public GameObject myLocation;
    [SerializeField] private int moveSpeed;
    [SerializeField] private Vector3 yOffset;
    private int remainingMove;
    UnitControllerScript UCS;


    // Start is called before the first frame update
    void Start()
    {

        if (this.gameObject.GetComponent< Cube_Multiplayer_Script>().serializedValue == 1 )
        {
            GameObject hex1 = GameObject.Find("Hex(0, 0, 0)");
            DeployToHex(hex1);
        }
        else
        {
            GameObject hex1 = GameObject.Find("Hex(9, 0, 0)");
            DeployToHex(hex1);
        }
        
        remainingMove = moveSpeed;
        UCS = GameObject.Find("UnitController").GetComponent<UnitControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        GameObject.Find("UnitController").GetComponent<UnitControllerScript>().changeActiveUnit(this.gameObject);
    }

    void DeployToHex(GameObject hex)
    {
        Debug.Log("DeployToHex Called");
        this.transform.position = hex.transform.position + yOffset;
        myLocation = hex;
    }
    public void CheckHex(GameObject hex)
    {
        if (remainingMove >= 1 && hex != myLocation)
        {
            MoveToHex(hex);
        }
    }
    public void MoveToHex(GameObject hex)
    {
        Debug.Log("MoveToHex Called");
        this.transform.position = hex.transform.position + yOffset;
        remainingMove -= 1;
        myLocation = hex;
        UCS.updateMoveables(myLocation.GetComponent<HexCell>());

    }

        
}
