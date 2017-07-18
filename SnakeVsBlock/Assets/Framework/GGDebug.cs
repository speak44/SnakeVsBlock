using UnityEngine;
using System.Collections;

public class GGDebug {

    public static bool EnableDebug = true;

    public static void Log(string context)
    {
        if (EnableDebug)
        {
            Debug.Log(context);
        }
    }

    public static void LogError(string context)
    {
        if (EnableDebug)
        {
            Debug.LogError(context);
        }
    }
}
