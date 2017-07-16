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

}
