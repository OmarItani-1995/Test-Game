using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour, IGrid
{
    void Awake()
    {
        DI.Register<IGrid>(this);
    }

    [SerializeField] private GameObject prefab;
    [SerializeField] private int Rows;
    [SerializeField] private int Columns;
    [SerializeField] private float spacing;

    private IGridPoint[,] gridObjects;

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
                gridObjects[r, c] = obj.GetComponent<IGridPoint>();
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

    public IGridPoint[,] GetPoints()
    {
        foreach (var ob in gridObjects)
        {
            Debug.Log(ob);
        }
        return gridObjects;
    }
    
    public Vector3 GetMinimumPoint()
    {
        if (gridObjects == null || gridObjects.Length == 0)
        {
            return Vector3.zero;
        }
        return gridObjects[0, 0].transform.position;
    }

#if UNITY_EDITOR
    [ContextMenu("Generate Grid")]
    private void Co_GenerateGrid()
    {
        GenerateGrid(Rows, Columns);
    }
    #endif
}

public interface IGrid
{
    void GenerateGrid(int rows, int columns);
    void ClearGrid();
    IGridPoint[,] GetPoints();
    Vector3 GetMinimumPoint();
}



