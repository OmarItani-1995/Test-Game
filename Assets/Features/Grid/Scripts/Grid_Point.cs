using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Point : MonoBehaviour, IGridPoint
{
    public GameObject gameObject
    {
        get { return ((Component)this).gameObject; }
    }
    public Transform transform
    {
        get { return ((Component)this).transform; }
    }
}

public interface IGridPoint
{
    public GameObject gameObject { get; }
    public Transform transform { get; }
}
