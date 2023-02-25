using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HexCell : MonoBehaviour {
    [SerializeField] private int x = 0;
    [SerializeField] private int y = 0;
    [SerializeField] private int z = 0;

    [SerializeField] private bool isActive = false;

    private static string dynamicBaseName = "Hex";

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    MeshRenderer m_Renderer;
    Color m_MouseOverColor = Color.red;
    Color m_OriginalColor;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<MeshRenderer>();

        m_OriginalColor = m_Renderer.material.color;
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
        m_Renderer.material.color = m_MouseOverColor;
        if (Input.GetMouseButtonDown(0)) {
            SetActive(!isActive);
            if (isActive) {
                HexGrid.Instance.AddActiveCell(this);
            } else {
                HexGrid.Instance.RemoveActiveCell(this);
            }
        }
    }

    void OnMouseExit() {
        // Reset the color of the GameObject back to normal
        if (!isActive) {
            m_Renderer.material.color = m_OriginalColor;
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
        string str = "(" + x + ", " + y + ")";
        UnityEditor.Handles.Label(transform.position, str);
        #endif
    }
}
