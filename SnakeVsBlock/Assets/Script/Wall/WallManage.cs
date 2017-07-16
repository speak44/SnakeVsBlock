using UnityEngine;
using System.Collections;

public class WallManage: MonoBehaviour  {

    private static WallManage instance = null;

    public int curCreateWallIndex = 9; //默认从第七排创建

    //固定x轴坐标
    //墙
    private int[] fWallPosX = { -300, -150, 0, 150, 300 };
    private int[] fFrancePosX = { -225, -75, 75, 225 };

    void Awake()
    {
        if (instance)
        {
            Debug.LogError("Constructor twice!!!");
        }
        instance = this;
    }

    public static WallManage GetInstance()
    {
        return instance;
    }

    //初始一排墙:值范围是1-2
    //权重比
    private float[] initHorizontalRate = {0.5f, 0.5f};
    public void CreateInitHorizontalWall()
    {
        for (int i=0; i<fWallPosX.Length; i++)
        {
            WallUnitController script = WallUnitController.Create();
            this.AddWallUnit(script.gameObject, this.GetWallPos(fWallPosX[i]));
        }
    }

    private Vector3 GetWallPos(int posX)
    {
        return new Vector3(posX, curCreateWallIndex * Global.wallHeight / 2);
    }

    private void AddWallUnit(GameObject child, Vector3 pos)
    {
        if (gameObject != null)
        {
            Vector3 orginScale = child.transform.localScale;
            child.transform.parent = gameObject.transform;
            child.transform.localScale = orginScale;
            child.transform.localPosition = pos;
        }
        else
        {
            Debug.Log("=================mGrid是空的，哪里出了问题了==================");
        }
    }
}
