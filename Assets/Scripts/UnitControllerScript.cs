using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControllerScript : MonoBehaviour
{
    public GameObject activeUnit;
    public bool movementPhase;
    HexGrid HexGrid;
    // Start is called before the first frame update
    void Start()
    {
        movementPhase = true;
        HexGrid = GameObject.Find("HexGrid(Clone)").GetComponent<HexGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeActiveUnit(GameObject newActiveUnit)
    {
        activeUnit = newActiveUnit;
        if (movementPhase)
        {
            HexCell currentCell = activeUnit.GetComponent<UnitMoveScript>().myLocation.GetComponent<HexCell>();
            List<HexCell> neighborList = HexGrid.GetNeighbors(currentCell);
            HexGrid.changeMovables(neighborList);
        }
    }

    public void updateMoveables(HexCell newLocation)
    {
        List<HexCell> neighborList = HexGrid.GetNeighbors(newLocation);
        HexGrid.changeMovables(neighborList);
    }
}
