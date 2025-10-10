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

    [Header("Fixed Camera Settings")]
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float maxScale = 0.8f;
    [SerializeField] private float totalDistance = 10;
    
    private IGridPoint[,] gridObjects;
    private ICamera _camera;
    private ICardManager _cardManager;
    private IGameRules_Layout _gameRulesLayout;

    void Start()
    {
        _camera = DI.Get<ICamera>();
        _cardManager = DI.Get<ICardManager>();
        _gameRulesLayout = DI.Get<IGameRules_Layout>();
    }

    public void GenerateGrid(int rows, int columns)
    {
        if (_camera.Mode == Camera_Mode.Fixed)
        {
            GenerateGridWithFixedCamera(rows, columns);
        }
        else
        {
            GenerateGridWithMovingCamera(rows, columns);
        }
    }

    private void GenerateGridWithFixedCamera(int rows, int columns)
    {
        Camera cam = _camera.GetCamera();

        Rows = rows;
        Columns = columns;

        float spacingFactor = 1.2f;
        int max = Mathf.Max(columns, rows);
        float scale = totalDistance / (max + (max - 1) * spacingFactor);
        float spacing = scale * spacingFactor;
        scale = Mathf.Clamp(scale, minScale, maxScale);
        spacing = scale * spacingFactor;
        
        
        float width = (columns - 1) * spacing;
        float height = (rows - 1) * spacing;
        Vector3 offset = new Vector3(-width / 2f, 0, -height / 2f);

        gridObjects = new Grid_Point[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Vector3 position = new Vector3(c * spacing, 0, r * spacing) + offset;
                GameObject obj = Instantiate(prefab, position, Quaternion.identity, transform);
                obj.name = $"Point_{r}_{c}";
                obj.transform.localScale = Vector3.one * scale;
                gridObjects[r, c] = obj.GetComponent<IGridPoint>();
            }
        }
        
        _cardManager.CardScale = scale;
    }

    private void GenerateGridWithMovingCamera(int rows, int columns)
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

    public Vector3 GetMaximumPoint()
    {
        if (gridObjects == null || gridObjects.Length == 0)
        {
            return Vector3.zero;
        }

        return gridObjects[Rows - 1, Columns - 1].transform.position;
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
    Vector3 GetMaximumPoint();
}