using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Rows;
    public int Columns;

    private IGrid grid;
    void Start()
    {
        grid = DI.Get<IGrid>();
    }
}
