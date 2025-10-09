using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : ValueLerp<Color>
{
    protected override Color GetValue()
    {
        if (timer < 0.5f)
        {
            return Color.Lerp(_from, _mid, timer * 2);
        }
        else
        {
            return Color.Lerp(_mid, _to, (timer - 0.5f) * 2);
        }
    }
}