using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class HexCellDto {
    public HexCellDto() { }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public bool IsActive { get; set; }
}