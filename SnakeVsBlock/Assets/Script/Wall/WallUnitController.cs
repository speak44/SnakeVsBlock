using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WallUnitController : MonoBehaviour {

    private const string PREFAB_PATH = "Prefab/WallUnit";

    public Text lifeNumText;

    private int lifeNum;

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

    void OnTriggerEnter2D(Collider2D cell)
    {
        GGDebug.Log("-----墙：---开始碰撞-------");
        DelLifeNum();
    }

    public void Refresh()
    {
        if (lifeNumText != null)
        {
            lifeNum = Random.Range(1, 51);
            lifeNumText.text = lifeNum.ToString();
        }
    }

    public void DelLifeNum()
    {
        lifeNum -= 1;
        lifeNumText.text = lifeNum.ToString();
        if (lifeNum == 0)
            Destroy(gameObject);
    }
}
