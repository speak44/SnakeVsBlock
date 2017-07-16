using UnityEngine;
using System.Collections;

public class WallUnitController : MonoBehaviour {

    private const string PREFAB_PATH = "Prefab/WallUnit";

    public static WallUnitController Create()
    {
        GameObject go = Instantiate(Resources.Load(PREFAB_PATH)) as GameObject;
        WallUnitController script = go.GetComponent<WallUnitController>();
        return script;
    }
}
