using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ValueLerp<T>
{
    protected T _from;
    protected T _mid;
    protected T _to;
    protected float _time;
    protected System.Action<T> _onSetValue;
    
    protected bool isLerping = false;
    protected float timer = 0;

    public void Start(T from, T mid, T to, float time, System.Action<T> onSetValue)
    {
        this._from = from;
        this._mid = mid;
        this._to = to;
        this._time = time;
        this._onSetValue = onSetValue;
        timer = 0;
        isLerping = true;
    }

    public void Update()
    {
        if (isLerping)
        {
            timer += Time.deltaTime / _time;
            if (timer >= 1)
            {
                timer = 1;
                isLerping = false;
            }
            _onSetValue(GetValue());
        }
    }

    protected abstract T GetValue();
}
