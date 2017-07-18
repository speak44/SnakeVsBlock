using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallManage: MonoBehaviour  {

    private static WallManage instance = null;

    public int curCreateWallIndex; //当前准备创建行的行数
    public Global.WallHorizontalType curHorizontalType = Global.WallHorizontalType.INIT; //当前行要创建的类型:
    private int loopIndexNum = 0; //两种形式: 1.7排以上 2.8排以上

    //固定x轴坐标
    //墙
    private int[] fWallPosX = { -300, -150, 0, 150, 300 };
    private int[] fFrancePosX = { -225, -75, 75, 225 };

    //权重
    private float[] initHorizontalRate = { 50f, 50f };
    private int[] wallNum = { 0, 1, 2 }; //墙数目:0-2
    public float[] wallNumRate = {50f, 80f, 100f}; //墙数目:0-2权重
    private int[] franceNum = { 0, 1, 2, 3 }; //栅栏数目:0-3权重
    public float[] franceNumRate = { 40f, 30f, 20f, 10f }; //栅栏数目:0-3权重

    

    void Awake()
    {
        if (instance)
        {
            GGDebug.LogError("Constructor twice!!!");
        }
        instance = this;

        //初始
        this.curCreateWallIndex = Mathf.CeilToInt((Screen.height / 2 + Global.wallHeight * 2) / Global.wallHeight);
    }

    public static WallManage GetInstance()
    {
        return instance;
    }

    //初始一排墙:值范围是1-2
    public void CreateInitHorizontalWall()
    {
        for (int i=0; i<fWallPosX.Length; i++)
        {
            WallUnitController script = WallUnitController.Create();
            this.AddWallUnit(script.gameObject, this.GetWallPos(fWallPosX[i]));
        }
        this.loopIndexNum++;
        
        //4行空墙
        for (int i=0; i<4; i++)
        {
            this.CreateNoneWall();
            this.loopIndexNum++;
        }

        this.TransformHorizontalWallType(Global.WallHorizontalType.SINGLE_WALL);
    }

    //空墙
    private void CreateNoneWall()
    {
        /*----
         * 先建空墙，后this.curCreateWallIndex++
        /**/

        this.curCreateWallIndex++;
    }

    public void CreateFrance()
    {
        int randNum = Random.Range(1, 3);
        if (randNum == 1)
            this.CreateFrance1();
        else if (randNum == 2)
            this.CreateFrance2();
    }
    
    //单格栅栏
    private void CreateFrance1()
    {
        GGDebug.Log("==================单格栅栏===================");
        //1.确定数目
        int num = this.GetRateNum(franceNum, franceNumRate);
        //2.确定位置
        for (int i=0; i<num; i++)
        {

        }

        this.loopIndexNum++;
        this.curCreateWallIndex++;
        this.JudgeIsNewFullWall();
    }

    //双格栅栏
    private void CreateFrance2()
    {
        this.loopIndexNum++;
        GGDebug.Log("==================双格栅栏===================");
        this.curCreateWallIndex+=2;
        this.JudgeIsNewFullWall();
    }

    //零散墙
    public void CreateSingleWall()
    {
        //1.确定生成的数目
        int num = this.GetRateNum(wallNum, wallNumRate);
        //2.确定位置
        if (num == 0)
        {
            GGDebug.Log("==================零散墙===================" + "0");
        }
        else if (num == 1)
        {
            int posIndex = Random.Range(1, 6);
            WallUnitController script = WallUnitController.Create();
            this.AddWallUnit(script.gameObject, this.GetWallPos(fWallPosX[posIndex-1]));
            GGDebug.Log("==================零散墙===================" + "1");
        }
        else if (num == 2)
        {
            int posIndex1 = Random.Range(1, 6);
            int posIndex2 = this.GetDontSideNum(posIndex1);

            WallUnitController script = WallUnitController.Create();
            this.AddWallUnit(script.gameObject, this.GetWallPos(fWallPosX[posIndex1 - 1]));

            script = WallUnitController.Create();
            this.AddWallUnit(script.gameObject, this.GetWallPos(fWallPosX[posIndex2 - 1]));
            GGDebug.Log("==================零散墙===================" + "2");
        }

        this.curCreateWallIndex++;
        this.loopIndexNum++;
        this.curHorizontalType = Global.WallHorizontalType.FRANCE;
    }

    //满墙
    public void CreateFullWall()
    {
        GGDebug.Log("==================满墙===================");
        for (int i = 0; i < fWallPosX.Length; i++)
        {
            WallUnitController script = WallUnitController.Create();
            this.AddWallUnit(script.gameObject, this.GetWallPos(fWallPosX[i]));
        }
        this.curCreateWallIndex++;
        this.curHorizontalType = Global.WallHorizontalType.FRANCE;
    }

    //判断是否要生成满墙
    private void JudgeIsNewFullWall()
    { 
        if ((this.loopIndexNum>=7 || this.loopIndexNum>=14) && this.curHorizontalType == Global.WallHorizontalType.FRANCE)
        {
            if (this.loopIndexNum>=7 && this.loopIndexNum<14)
            {
                int randNum = Random.Range(1, 3);
                if (randNum == 1)
                {
                    this.curHorizontalType = Global.WallHorizontalType.SINGLE_WALL;
                    
                }
                else
                {
                    this.curHorizontalType = Global.WallHorizontalType.FULL_WALL;
                    this.loopIndexNum = 0;
                }
            }
            else
            {
                this.curHorizontalType = Global.WallHorizontalType.FULL_WALL;
                this.loopIndexNum = 0;
            }
        }
        else
            this.curHorizontalType = Global.WallHorizontalType.SINGLE_WALL;
    }

    //得到不相邻的数
    private int GetDontSideNum(int num)
    {
        while(true)
        {
            int randNum = Random.Range(1, 6);
            if (randNum != num && Mathf.Abs(randNum - num) != 1)
                return randNum;
        }
    }

    //得到权重随机数目
    private int GetRateNum(int[] numArr, float[] rateArr)
    {
        int randRateNum = Random.Range(1, 101);
        for (int i=0; i<rateArr.Length; i++)
        {
            if (randRateNum <= rateArr[i])
            {
                return numArr[i];
            }
        }

        return 0;
    }

    private Vector3 GetWallPos(int posX)
    {
        return new Vector3(posX, curCreateWallIndex * Global.wallHeight-Global.wallHeight / 2);
    }

    //转换当前行要创建的类型
    public void TransformHorizontalWallType(Global.WallHorizontalType type)
    {
        this.curHorizontalType = type;
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
            GGDebug.Log("=================mGrid是空的，哪里出了问题了==================");
        }
    }
}
