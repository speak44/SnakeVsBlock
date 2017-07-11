using UnityEngine;
using System.Collections;

public class SnakeManage {

    private static SnakeManage instance = null;

    public static SnakeManage GetInstance()
    {
        if (instance == null)
            instance = new SnakeManage();
        return instance;
    }
}
