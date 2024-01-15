using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static GameObject FindChild(this GameObject go, string name) => Util.FindChild(go, name);
    public static T FindChild<T>(this GameObject go, string name) where T : UnityEngine.Object => Util.FindChild<T>(go, name);
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component => Util.GetOrAddComponent<T>(go);

    public static bool IsValid(this GameObject go)
    {
        return go != null && go.activeSelf;
    }
}
