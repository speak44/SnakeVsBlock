using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TouchEventListener : EventTrigger
{
    
    
    public override void OnDrag(PointerEventData eventData)
    {
        //Vector3 newPos = SnakeManage.GetInstance().mainCamera.WorldToScreenPoint(SnakeManage.GetInstance().snakeObj.transform.localPosition);
        //Debug.Log("==============x================== " + eventData.position.x);
        //Debug.Log("================y================ " + eventData.position.y);
        
        //Debug.Log("==============x================== " + newPos.x);
        //Debug.Log("================y================ " + SnakeManage.GetInstance().snakeObj.transform.localPosition.y);
    }
}
