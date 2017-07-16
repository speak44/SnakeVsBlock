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
            Debug.LogError("======================================Constructor twice!!!===================================");
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

        //墙
        Vector3 curSnakePos = SnakeManage.GetInstance().GetLifeNumTextPos();
        Debug.Log("=======================================================" + Screen.height / 2);
        if ((WallManage.GetInstance().curCreateWallIndex*Global.wallHeight/2 - curSnakePos.y) <= Screen.height/2) //创建新的一排障碍物
        {
            //Debug.Log("=======================================" + (WallManage.GetInstance().curCreateWallIndex * Global.wallHeight / 2 - curSnakePos.y));
            WallManage.GetInstance().CreateInitHorizontalWall();
            WallManage.GetInstance().curCreateWallIndex++;
            //if (WallManage.GetInstance().curCreateWallIndex == 7) //初始第一排
            //{
            //WallManage.GetInstance().CreateInitHorizontalWall();
            //}
            //else
            //{

            //}
        }
    }
}
