using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HexCell : MonoBehaviour {
    [SerializeField] private int x = 0;
    [SerializeField] private int y = 0;
    [SerializeField] private int z = 0;

    [SerializeField] private bool isActive = false;

    [SerializeField] List<Material> materials = new List<Material>();
    [SerializeField] Mesh originalMesh;
    [SerializeField] Mesh activeMesh;
    [SerializeField] GameObject heightHexCell;

    private static string dynamicBaseName = "Hex";
    private List<GameObject> heightCells = new List<GameObject>();

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    [SerializeField] private MeshRenderer m_Renderer;
    Color m_colorMouseOver = Color.red;

    Color m_OriginalColor;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<MeshRenderer>();

        m_OriginalColor = m_Renderer.material.color;

        originalMesh = GetComponent<Mesh>();

        m_Renderer.SetMaterials(materials);
    }
     
     void Update() {

    }

    public bool IsActive() {
        return isActive;
    }

    public Color GetColor() {
        return m_Renderer.material.color;
    }

    public void SetActive(bool active) {
        if (active) {
            var meshInstance = Instantiate(activeMesh);
            GetComponent<MeshFilter>().mesh = meshInstance;
        }

        isActive = active;
    }
         
    void OnMouseOver() {
        m_Renderer.material.color = m_colorMouseOver;

        //Right Mouse Button
        if (Input.GetMouseButtonDown(0)) {
            if (isActive) {
                AddHeight();
            } else {
                SetActive(true);
                HexGrid.Instance.AddActiveCell(this);
            }

            var meshInstance = Instantiate(activeMesh);
            GetComponent<MeshFilter>().mesh = meshInstance;
        }

        //Left Mouse Button
        if (Input.GetMouseButtonDown(1)) {
            if (isActive) {
                SetActive(false);
                HexGrid.Instance.RemoveActiveCell(this);
                foreach(GameObject heightCell in heightCells) {
                    Destroy(heightCell);
                    transform.position = new Vector3(transform.position.x, transform.position.y - HexCellOffset.zOffset, transform.position.z);
                }
                GetComponent<MeshFilter>().mesh = originalMesh;
                heightCells = new List<GameObject>();
                z = 0;
                this.name = dynamicBaseName + "(" + x + ", " + y + ", " + z + ")";
            }
        }
    }

    public void AddHeight() {
        CreateHeightHex(z);
        z += 1;
        this.name = dynamicBaseName + "(" + x + ", " + y + ", " + z + ")";
    }

    public void SetHeight() {
        heightCells = new List<GameObject>();
        for (int heights = 0; heights < z; heights++) {
            CreateHeightHex(heights);
        }
    }

    public void SetName() {
        this.name = dynamicBaseName + "(" + x + ", " + y + ", " + z + ")";
    }

    private void CreateHeightHex(int heightLayer) {
        GameObject newHex = Instantiate(heightHexCell, this.transform);

        float positionY;
        if (heightCells.Count > 0) {
            positionY = heightCells.ElementAt(heightLayer - 1).transform.position.y;
        } else {
            positionY = newHex.transform.position.y;
        }

        newHex.transform.position = new Vector3(newHex.transform.position.x, positionY - HexCellOffset.zOffset, newHex.transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + HexCellOffset.zOffset, transform.position.z);
        heightCells.Add(newHex);
    }

    void OnMouseExit() {
        m_Renderer.material.color = m_OriginalColor;
    }

    public int getX() {
        return x;
    }

    public void setX(int x) {
        this.x = x;
    }

    public int getY() {
        return y;
    }

    public int getZ() {
        return z;
    }

    public void setY(int y) {
        this.y = y;
    }

    public void setZ(int z) {
        this.z = z;
    }

    public static HexCell CreateHexCell(HexCell hexCellPrefab, int x, int y, int z, Transform parent, Vector3 previousCellPosition, HexCellOffset.Offset offset) {
        HexCell newHexCell = Instantiate(hexCellPrefab, HexCellOffset.GetTransform(previousCellPosition, offset), Quaternion.identity, parent);

        newHexCell.setX(x);
        newHexCell.setY(y);
        newHexCell.setZ(z);
        newHexCell.name = dynamicBaseName + "(" + x + ", " + y + ", " + z + ")";

        return newHexCell;
    }

    void OnDrawGizmos() {
        #if UNITY_EDITOR
        UnityEditor.Handles.color = Color.white;
        string str = "(" + x + ", " + y + ", " + z + ")";
        UnityEditor.Handles.Label(transform.position, str);
        #endif
    }
}
