using UnityEngine;
using System.Collections;

public class Global {

    private static Global instance = null;

    public const int wallHeight = 144; //墙的宽高
    public const int snakeUnitWidth = 38; //球直径
    public const int offsetHeight = 45; //生命文本


    public static Global GetInstance()
    {
        if (instance == null)
            instance = new Global();
        return instance;
    }

    //每一行的类型
    public enum WallHorizontalType
    {
        NONE = 0, //空行
        INIT, //初始行
        FRANCE, //
        FRANCE_1, //竖直一格的栅栏
        FRANCE_2, //竖直两格的栅栏
        SINGLE_WALL, //零散墙 
        FULL_WALL, //一排墙
    }

}
