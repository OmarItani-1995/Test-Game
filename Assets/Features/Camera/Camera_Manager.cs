using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour, ICamera
{
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Transform StartingPoint;
    [SerializeField] private Transform EndingPoint;
    [SerializeField] private Camera camera;
    [SerializeField] private Camera_Mode mode = Camera_Mode.Movable;
    
    public Camera_Mode Mode => mode;
    
    private IGameRules_Layout _gameRules;
    private Vector3 _cameraPosition;
    void Awake()
    {
        DI.Register<ICamera>(this);
    }

    void Start()
    {
        _gameRules = DI.Get<IGameRules_Layout>();
        _cameraPosition = StartingPoint.position;
    }
    
    public void MoveToView(int rows, int columns)
    {
        float rowPercent = (float)rows / _gameRules.MaxRows;
        float columnPercent = (float)columns / _gameRules.MaxColumns;

        _cameraPosition = Vector3.Lerp(StartingPoint.position, EndingPoint.position, Mathf.Max(rowPercent, columnPercent));
    }

    public Camera GetCamera()
    {
        return camera;
    }

    void Update()
    {
        CameraTransform.position = Vector3.Lerp(CameraTransform.position, _cameraPosition, Time.deltaTime * 2);
    }
}

public enum Camera_Mode
{
    Movable, Fixed
}

public interface ICamera
{
    Camera_Mode Mode { get; }
    void MoveToView(int rows, int columns);
    Camera GetCamera();
}
