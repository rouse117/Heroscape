 using UnityEditor;
using UnityEngine;
/*
public class HexGridEditor : EditorWindow
{
    [MenuItem("Window/Hex Grid Editor")]
    public static void ShowWindow()
    {
        GetWindow<HexGridEditor>("Hex Grid Editor");
    }

    private GameObject hexPrefab;

    private int sizeX = 10;
    private int sizeY = 10;
<<<<<<< HEAD
=======
    private int sizeZ = 1;
>>>>>>> a571d3e (Create HexGrid through Window/Hex Grid Editor)

    private void OnGUI()
    {
        GUILayout.Label("Hexagon Grid Settings", EditorStyles.boldLabel);

        hexPrefab = (GameObject)EditorGUILayout.ObjectField("Hexagon Prefab", hexPrefab, typeof(GameObject), false);

        sizeX = EditorGUILayout.IntField("Size X", sizeX);
        sizeY = EditorGUILayout.IntField("Size Y", sizeY);
<<<<<<< HEAD

        if (GUILayout.Button("Create Blank Hexagon Grid"))
        {
            CreateHexagonGrid(false);
        }

        if (GUILayout.Button("Create Saved Hexagon Grid")) {
            CreateHexagonGrid(true);
        }
    }

    private void CreateHexagonGrid(bool savedVersion)
=======
        sizeZ = EditorGUILayout.IntField("Size Z", sizeZ);

        if (GUILayout.Button("Create Hexagon Grid"))
        {
            CreateHexagonGrid();
        }
    }

    private void CreateHexagonGrid()
>>>>>>> a571d3e (Create HexGrid through Window/Hex Grid Editor)
    {
        if (hexPrefab == null)
        {
            Debug.Log("Hexagon Prefab is not set!");
            return;
        }

        GameObject hexGridObject = Instantiate(hexPrefab);
        HexGrid hexGrid = hexGridObject.GetComponent<HexGrid>();
<<<<<<< HEAD
        hexGrid.setPlayMode(savedVersion);

        if (savedVersion) {
            hexGrid.Restore();
        } else {
            hexGrid.GenerateHexGrid(sizeX, sizeY);
        }

=======
        hexGrid.GenerateHexGrid(sizeX, sizeY, sizeZ);
>>>>>>> a571d3e (Create HexGrid through Window/Hex Grid Editor)
    }
}
*/