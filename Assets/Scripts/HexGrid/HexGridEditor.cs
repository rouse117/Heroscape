 using UnityEditor;
using UnityEngine;

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
    private int sizeZ = 1;

    private void OnGUI()
    {
        GUILayout.Label("Hexagon Grid Settings", EditorStyles.boldLabel);

        hexPrefab = (GameObject)EditorGUILayout.ObjectField("Hexagon Prefab", hexPrefab, typeof(GameObject), false);

        sizeX = EditorGUILayout.IntField("Size X", sizeX);
        sizeY = EditorGUILayout.IntField("Size Y", sizeY);
        sizeZ = EditorGUILayout.IntField("Size Z", sizeZ);

        if (GUILayout.Button("Create Hexagon Grid"))
        {
            CreateHexagonGrid();
        }
    }

    private void CreateHexagonGrid()
    {
        if (hexPrefab == null)
        {
            Debug.Log("Hexagon Prefab is not set!");
            return;
        }

        GameObject hexGridObject = Instantiate(hexPrefab);
        HexGrid hexGrid = hexGridObject.GetComponent<HexGrid>();
        hexGrid.GenerateHexGrid(sizeX, sizeY, sizeZ);
    }
}