using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using UnityEngine;
using Unity.VisualScripting;

[Serializable]
public class HexGridDto {
    private string fileName = "savedGrid.xml";

    private List<HexCellDto> hexCells = new List<HexCellDto>();
    private int sizeX;
    private int sizeY;

    public List<HexCellDto> GetHexCells() {
        return hexCells;
    }

    public int GetSizeX() {
        return sizeX;
    }

    public int GetSizeY() {
        return sizeY;
    }

    public void AddHexCells(HexGrid hexGrid) {

        foreach(HexCell hexCell in hexGrid.GetCells()) {
            hexCells.Add(new HexCellDto {
                X = hexCell.getX(),
                Y = hexCell.getY(),
                Z = hexCell.getZ(),
                IsActive = hexCell.IsActive(),
            });
        }
    }

    public void Serialize(HexGrid hexGrid) {
        AddHexCells(hexGrid);

        sizeX = hexGrid.GetSizeX();
        sizeY = hexGrid.GetSizeY();
        Serializer.Serialize(fileName, this);
    }

    public HexGridDto Deserialize() {
        return Serializer.Deserialize<HexGridDto>(fileName);
    }


}
