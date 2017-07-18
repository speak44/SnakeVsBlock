using UnityEngine;
using System.Collections;

public class FranceUnitController : MonoBehaviour {

    private const string PREFAB_PATH_1 = "Prefab/France1";
    private const string PREFAB_PATH_2 = "Prefab/France2";

    public static FranceUnitController Create(Global.WallHorizontalType franceType)
    {
        string prefabPath = PREFAB_PATH_1;
        if (franceType == Global.WallHorizontalType.FRANCE_1)
            prefabPath = PREFAB_PATH_1;
        else if (franceType == Global.WallHorizontalType.FRANCE_2)
            prefabPath = PREFAB_PATH_2;

        GameObject go = Instantiate(Resources.Load(prefabPath)) as GameObject;
        FranceUnitController script = go.GetComponent<FranceUnitController>();
        return script;
    }

    public void Refresh()
    {
    }
}
