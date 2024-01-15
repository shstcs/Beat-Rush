using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util
{
    public static T FindChild<T>(GameObject go, string name = null) where T : Object
    {
        if (go == null)
            return null;

        T[] components = go.GetComponentsInChildren<T>(true);
        if (string.IsNullOrEmpty(name))
            return components[0];
        else
            return components.Where(x => x.name == name).FirstOrDefault();
    }

    public static T FindChildDirect<T>(GameObject go, string name = null) where T : Object
    {
        if (go == null)
            return null;
        for(int i =0;i<go.transform.childCount;i++)
        {
            Transform transform = go.transform.GetChild(i);
            if(string.IsNullOrEmpty(name) || transform.name == name)
            {
                if(transform.TryGetComponent<T>(out T component))
                    return component;
            }
        }
        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null)
    {
        Transform transform = FindChild<Transform>(go, name);
        if (transform == null)
            return null;
        return transform.gameObject;
    }

    public static GameObject FindChildDriect(GameObject go, string name = null)
    {
        Transform transform = FindChildDirect<Transform>(go, name);
        if (transform == null)
            return null;
        return transform.gameObject;
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        if(!go.TryGetComponent<T>(out T component))
            component = go.AddComponent<T>();
        return component;
    }
}
