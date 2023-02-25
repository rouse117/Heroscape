using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HexGrid : MonoBehaviour
{
    // Singleton instantiation
    private static HexGrid instance;
    public static HexGrid Instance {
        get {
            if (instance == null) instance = FindObjectOfType<HexGrid>();
            return instance;
        }
    }

    [SerializeField] HexCell hexCellPrefab;
    [SerializeField] HexCell baseHexCell;

    [SerializeField] int xSize = 10;
    [SerializeField] int ySize = 10;
    [SerializeField] int zSize = 1;

    [SerializeField] List<HexCell> cells = new List<HexCell>();
    [SerializeField] List<HexCell> axtiveCells = new List<HexCell>();

    void Start() {
        GenerateHexGrid();
    }

    private HexCell FindCell(int x, int y) {
        foreach(HexCell cell in cells) {
            if (cell.getX() == x && cell.getY() == y) {
                return cell;
            }
        }

        return hexCellPrefab;
    }

    public void AddActiveCell(HexCell cell) {
        axtiveCells.Add(cell);
    }

    public void RemoveActiveCell(HexCell cell) {
        axtiveCells.Remove(cell);
    }

    private void GenerateHexGrid() {
        HexCell previousHexCell = hexCellPrefab;
        for (int z = 0; z < zSize; ++z) {
            for (int x = 0; x < xSize; ++x) {
                for (int y = 0; y < ySize; ++y) {
                    if (y == 0) {
                        previousHexCell = GenerateTop(x, y, z);
                    } else {
                        previousHexCell = GenerateHexCell(x, y, z, previousHexCell.transform.position, HexCellOffset.Offset.offset3);
                    }
                }
            }
        }
    }

    private HexCell GenerateTop(int x, int y, int z) {
        bool xIsEven = x % 2 == 0;
        Vector3 foundCellPosition = FindCell(x - 1, 0).transform.position;

        if (xIsEven) {
            return GenerateHexCell(x, y, z, foundCellPosition, HexCellOffset.Offset.offset1);
        } else {
            return GenerateHexCell(x, y, z, foundCellPosition, HexCellOffset.Offset.offset2);
        }
    }

    private HexCell GenerateHexCell(int x, int y, int z, Vector3 position, HexCellOffset.Offset offset) {
        HexCell newHexCell = HexCell.CreateHexCell(hexCellPrefab, x, y, z, this.transform, position, offset);
        cells.Add(newHexCell);
        return newHexCell;
    }

    // Update is called once per frame
    void Update() {

    }
}
