using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TouchEventListener : EventTrigger
{

    public override void OnDrag(PointerEventData eventData)
    {
        Debug.Log("==============Screen.width================== " + Screen.width);
        Debug.Log("================Screen.height================ " + Screen.height);
        //Debug.Log("============ " + eventData.pointerCurrentRaycast.worldPosition.x);
    }
}
