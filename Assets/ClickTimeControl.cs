using UnityEngine;
using System.Collections;
using System;

public class ClickTimeControl : MonoBehaviour
{
    public float maxMS = 1500.0f;

    public delegate void ClickTimeHandler(float percentage);
    public event ClickTimeHandler Click;

    private DateTime initialTime;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
#if UNITY_ANDROID
#else
        if (Input.GetMouseButtonDown(0))
        {

            // Saving initial time
            initialTime = DateTime.Now;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            var heldTime = DateTime.Now - initialTime;
            if(heldTime.TotalMilliseconds > maxMS)
            {
                Click(1.0f);
            }
            else
            {
                Click((float)heldTime.TotalMilliseconds / maxMS);
            }
            
        }
#endif
    }

}
