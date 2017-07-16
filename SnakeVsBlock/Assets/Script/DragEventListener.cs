using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragEventListener : EventTrigger
{
    public float xSpeedRate = 1f;

    private bool _isPress;
    private Vector3 _lastTouchPos;
    public static float xSpeed = 0;

    public override void OnPointerDown(PointerEventData eventData)
    {
        _isPress = true;
        _lastTouchPos = eventData.position;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        _isPress = false;
        xSpeed = 0;
    }

    void FixedUpdate()
    {
        if (_isPress)
        {
            xSpeed = (Input.mousePosition.x - _lastTouchPos.x) / Time.fixedDeltaTime * xSpeedRate;
            _lastTouchPos = Input.mousePosition;
        }
    }
}
