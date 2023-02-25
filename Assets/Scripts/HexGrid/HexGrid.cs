using System;
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

    [SerializeField] int sizeX = 10;
    [SerializeField] int sizeY = 10;
    [SerializeField] int sizeZ = 1;

    [SerializeField] List<HexCell> cells = new List<HexCell>();
    [SerializeField] List<HexCell> axtiveCells = new List<HexCell>();

    void Start() {
        if (transform.GetComponentsInChildren<Transform>().Length == 1) {
            GenerateHexGrid(sizeX, sizeY, sizeZ);
        }
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

    public void GenerateHexGrid(int sizeX = 0, int sizeY = 0, int sizeZ = 0) {
        HexCell previousHexCell = hexCellPrefab;
        for (int z = 0; z < sizeZ; ++z) {
            for (int x = 0; x < sizeX; ++x) {
                for (int y = 0; y < sizeY; ++y) {
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
