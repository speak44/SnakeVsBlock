using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogicPartController : MonoBehaviour {

    private static LogicPartController instance = null;

    private float dragMoveDisX = 0f; //拖拽时X轴偏移量
    public float speed = 0.00000001f; //蛇向上移动的速度,2/deltaTime
    public float lerpRate = 0.1f;

    void Awake()
    {
        if (instance)
        {
            GGDebug.LogError("======================================Constructor twice!!!===================================");
        }
        instance = this;
        Application.targetFrameRate = 60;

    }

    public static LogicPartController GetInstance()
    {
        return instance;
    }

    float lastX = 0;
    void FixedUpdate()
    {
        Vector3 unitV = new Vector2(Mathf.Lerp(lastX, DragEventListener.xSpeed, lerpRate), speed);
        lastX = unitV.x;
        SnakeUnitController.moveSpeed = unitV.magnitude * Time.fixedDeltaTime;
        List<SnakeUnitController> snakeList = SnakeManage.GetInstance().GetSnakeList(); //头节点
        SnakeUnitController firstSnake = snakeList[0];
        firstSnake.transform.localPosition += unitV.normalized * SnakeUnitController.moveSpeed;

        //生命数目text
        SnakeManage.GetInstance().lifeNumText.transform.localPosition += unitV.normalized * SnakeUnitController.moveSpeed;

        //Debug.Log(SnakeUnit.moveSpeed);
        for (int i = 0; i < snakeList.Count; i++)
        {
            snakeList[i].MyUpdate();
        }

        //创造障碍物
        Vector3 curSnakePos = SnakeManage.GetInstance().GetLifeNumTextPos();
        if ((WallManage.GetInstance().curCreateWallIndex*Global.wallHeight/2 - curSnakePos.y) <= Screen.height/2) //创建新的一排障碍物
        {
            if (WallManage.GetInstance().curHorizontalType == Global.WallHorizontalType.INIT) //初始第一排
            {
                WallManage.GetInstance().CreateInitHorizontalWall();
            }
            else
            {
                if (WallManage.GetInstance().curHorizontalType == Global.WallHorizontalType.NONE) //空墙
                {
                    GGDebug.Log("=====================怎么创建空墙了==========================");
                    //WallManage.GetInstance().CreateNoneWall();
                }
                else if (WallManage.GetInstance().curHorizontalType == Global.WallHorizontalType.SINGLE_WALL) //零散墙
                {
                    WallManage.GetInstance().CreateSingleWall();
                }
                else if (WallManage.GetInstance().curHorizontalType == Global.WallHorizontalType.FRANCE) //单格栅栏 双格栅栏
                {
                    WallManage.GetInstance().CreateFrance();
                }
                else if (WallManage.GetInstance().curHorizontalType == Global.WallHorizontalType.FULL_WALL) //满墙
                {
                    WallManage.GetInstance().CreateFullWall();
                }
            }
        }
    }
}
