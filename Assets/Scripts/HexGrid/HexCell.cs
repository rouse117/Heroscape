using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HexCell : MonoBehaviour {
    [SerializeField] private int x = 0;
    [SerializeField] private int y = 0;
    [SerializeField] private int z = 0;

    [SerializeField] private bool isActive = false;

    [SerializeField] List<Material> materials = new List<Material>();
    [SerializeField] GameObject heightHexCell;

    private static string dynamicBaseName = "Hex";
    private List<GameObject> heightCells = new List<GameObject>();

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    MeshRenderer m_Renderer;
    Color m_colorLayer1 = Color.red;
    Color m_colorLayer2 = Color.blue;
    Color m_colorLayer3 = Color.yellow;

    Color m_OriginalColor;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<MeshRenderer>();

        m_OriginalColor = m_Renderer.material.color;

        m_Renderer.SetMaterials(materials);
    }
     
     void Update() {

    }

    public bool IsActive() {
        return isActive;
    }

    public void SetActive(bool active) {
        isActive = active;
    }
         
    void OnMouseOver() {
        // Change the color of the GameObject to red when the mouse is over GameObject
        m_Renderer.material.color = GetMouseOverColor(z);
        if (Input.GetMouseButtonDown(0)) {

            if (isActive) {
                AddHeight();
            } else {
                SetActive(true);
                m_Renderer.material.color = m_Renderer.material.color;
                HexGrid.Instance.AddActiveCell(this);
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            if (isActive) {
                SetActive(false);
                m_Renderer.material.color = m_OriginalColor;
                HexGrid.Instance.RemoveActiveCell(this);
                foreach(GameObject heightCell in heightCells) {
                    Destroy(heightCell);
                    transform.position = new Vector3(transform.position.x, transform.position.y - HexCellOffset.zOffset, transform.position.z);
                }
                z = 0;
                this.name = dynamicBaseName + "(" + x + ", " + y + ", " + z + ")";
            }
        }
    }

    private void AddHeight() {
        switch (z) {
            case 0:
                CreateHeightHex();
                m_Renderer.material.color = m_colorLayer2;
                break;
            default:
                CreateHeightHex();
                m_Renderer.material.color = m_colorLayer3;
                break;
        }
        z += 1;
        this.name = dynamicBaseName + "(" + x + ", " + y + ", " + z + ")";
    }

    private void CreateHeightHex() {
        GameObject newHex = Instantiate(heightHexCell, this.transform);

        float positionY;
        if (heightCells.Count > 0) {
            positionY = heightCells.ElementAt(z - 1).transform.position.y;
        } else {
            positionY = newHex.transform.position.y;
        }

        newHex.transform.position = new Vector3(newHex.transform.position.x, positionY - HexCellOffset.zOffset, newHex.transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + HexCellOffset.zOffset, transform.position.z);
        heightCells.Add(newHex);
    }

    private Color GetLowerLayerColor(int currentZ) {
        switch (currentZ) {
            case 0:
                return m_colorLayer1;
            case 1:
                return m_colorLayer2;
            case 2:
                return m_colorLayer3;
        }

        return m_colorLayer3;
    }

    public Color GetMouseOverColor(int z) {
        if (isActive) {
            switch (z) {
                case 0:
                    return m_colorLayer2;
                case 1:
                    return m_colorLayer3;
                default:
                    return m_colorLayer3;
            }
        }

        return m_colorLayer1;
    }

    void OnMouseExit() {
        if (!isActive) {
            m_Renderer.material.color = m_OriginalColor;
        } else {
            m_Renderer.material.color = GetLowerLayerColor(z);
        }
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
