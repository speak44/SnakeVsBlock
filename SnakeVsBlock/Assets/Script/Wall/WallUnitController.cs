using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WallUnitController : MonoBehaviour {

    private const string PREFAB_PATH = "Prefab/WallUnit";

    public Text lifeNumText;

    public static WallUnitController Create()
    {
        GameObject go = Instantiate(Resources.Load(PREFAB_PATH)) as GameObject;
        ////测试随机取一个颜色
        //Color randColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        //go.GetComponent<Image>().color = randColor;
        WallUnitController script = go.GetComponent<WallUnitController>();
        script.Refresh();
        return script;
    }

    public void Refresh()
    {
        if (lifeNumText != null)
            lifeNumText.text = Random.Range(0, 51).ToString();
    }
}
