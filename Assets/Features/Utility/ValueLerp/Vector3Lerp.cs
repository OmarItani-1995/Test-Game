using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Lerp : ValueLerp<Vector3>
{
    protected override Vector3 GetValue()
    {
        return (Mathf.Pow(1 - timer, 2) * _from) + (2 * (1 - timer) * timer * _mid) + (Mathf.Pow(timer, 2) * _to);
    }
}
