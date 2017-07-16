using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SnakeManage : MonoBehaviour {

    private static SnakeManage instance = null;

    public int initSnakeCouts = 5;
    private List<Vector3> mInitPos = new List<Vector3>(); //初始球的位置

    private List<SnakeUnitController> mSnakeList = new List<SnakeUnitController>(); //蛇节点
    private int lifeCounts = 0;
    public Text lifeNumText;
    public int curCellIndex = 0;

    void Awake()
    {
        if (instance)
        {
            Debug.LogError("Constructor twice!!!");
        }
        instance = this;

        for (int i=0; i<this.initSnakeCouts; i++)
        {
            mInitPos.Add(new Vector3(0f, 0f, 0f));
        }

        //计算

        //测试
        this.InitSnake();
    }

    public static SnakeManage GetInstance()
    {
        return instance;
    }

    public List<SnakeUnitController> GetSnakeList()
    {
        return mSnakeList;
    }

    //初始游戏5条命，数字显示从0开始
    public void InitSnake()
    {
        //初始生命5 Text
        this.lifeCounts = initSnakeCouts;
        this.SetLifeNumText();

        //添加初始位置
        for (int i = 0; i < mInitPos.Count; i++)
        {
            SnakeUnitController unit = SnakeUnitController.Create(mSnakeList.Count == 0 ? null : mSnakeList[i - 1], i);
            mSnakeList.Add(unit);
            this.AddSnakeUnit(unit.gameObject, mInitPos[i]);
        }
    }

    //设置生命数目Text
    public void SetLifeNumText()
    {
        if (lifeNumText == null)
        {
            Debug.Log("=================lifeNumText是空的，哪里出问题了====================");
            return;
        }
        else
        {
            lifeNumText.text = this.lifeCounts - 1 + "";
        }
    }

    public Vector3 GetLifeNumTextPos()
    {
        return lifeNumText.transform.localPosition;
    }

    /// <summary>
    /// 得到蛇的生命数目
    /// </summary>
    public int GetLifeCounts()
    {
        return this.lifeCounts;
    }

    /// <summary>
    /// 设置蛇的生命数目
    /// </summary>
    public void SetLifeCounts(int lifeCounts)
    {
        if (this.lifeCounts < 0)
        {
            Debug.Log("=============================lifeCount不可以赋值为负数=============================");
            return;
        }
        else
            this.lifeCounts = lifeCounts;
    }

    private void AddSnakeUnit(GameObject child ,Vector2 pos)
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

    //入队
    public void Enqueue(SnakeUnitController unit)
    {
        
    }

    //出队
    public void Dequeue()
    {

    }

}
