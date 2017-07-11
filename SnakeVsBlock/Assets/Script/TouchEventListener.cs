using UnityEngine;
using System.Collections;

public class TouchEventListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("=============1================" + Input.touchCount);
        if (Input.touchCount > 0)
        {
            Debug.Log("=============2================");
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Debug.Log("=============x================" + Input.GetTouch(0).position.x);
                Debug.Log("=============y================" + Input.GetTouch(0).position.y);
            }
        }
	}

    void OnGUI()
    {
        Debug.Log("============OnGUI================" + Input.touchCount);
    }
}
