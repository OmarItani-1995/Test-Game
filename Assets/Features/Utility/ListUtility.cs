using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListUtility
{
    public static T GetRandomAndRemove<T>(this List<T> list)
    {
        var index = Random.Range(0, list.Count);
        var item = list[index];
        list.RemoveAt(index);
        return item;
    }

    public static List<T> DuplicateContent<T>(this List<T> list)
    {
        var newList = new List<T>(list.Count*2);
        newList.AddRange(list);
        newList.AddRange(list);
        return newList;
    }
    
    public static List<T> Copy<T>(this List<T> list)
    {
        var newList = new List<T>(list.Count);
        newList.AddRange(list);
        return newList;
    }
}
