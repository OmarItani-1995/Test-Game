using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject prefab;
    public int Rows;
    public int Columns;
    public float spacing;

    private Grid_Point[,] gridObjects;

    public void GenerateGrid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        var offset = Vector3.zero;
        
        float width = (columns - 1) * spacing;
        float height = (rows - 1) * spacing;
        offset = new Vector3(-width / 2f, 0, -height / 2f);
        
        gridObjects = new Grid_Point[rows, columns];
        
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Vector3 position = new Vector3(c * spacing, 0, r * spacing) + offset;
                GameObject obj = Instantiate(prefab, position, Quaternion.identity, this.transform);
                obj.name = $"Point_{r}_{c}";
                gridObjects[r, c] = obj.GetComponent<Grid_Point>();
            }
        }
    }

    public void ClearGrid()
    {
        if (gridObjects != null)
        {
            foreach (var point in gridObjects)
            {
                if (point != null)
                {
                    DestroyImmediate(point.gameObject);
                }
            }
            gridObjects = null;
        }
    }
    
    #if UNITY_EDITOR
    [ContextMenu("Generate Grid")]
    private void Co_GenerateGrid()
    {
        GenerateGrid(Rows, Columns);
    }
    #endif
}

public class Grid_Point : MonoBehaviour
{
    
}
