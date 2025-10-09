using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour, ICamera
{
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Transform StartingPoint;
    [SerializeField] private Transform EndingPoint;
    
    private IGameRules_Layout _gameRules;

    private Vector3 _cameraPosition;
    void Awake()
    {
        DI.Register<ICamera>(this);
    }

    void Start()
    {
        _gameRules = DI.Get<IGameRules_Layout>();
    }
    
    public void MoveToView(int rows, int columns)
    {
        float rowPercent = (float)rows / _gameRules.MaxRows;
        float columnPercent = (float)columns / _gameRules.MaxColumns;

        _cameraPosition = Vector3.Lerp(StartingPoint.position, EndingPoint.position, Mathf.Max(rowPercent, columnPercent));
    }

    void Update()
    {
        CameraTransform.position = Vector3.Lerp(CameraTransform.position, _cameraPosition, Time.deltaTime * 2);
    }
}

public interface ICamera
{
    void MoveToView(int rows, int columns);
}
