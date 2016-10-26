using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour {

    public GameObject ball;
    public GameObject reticle;
    public float lowSpeed = 500.0f;
    public float highSpeed = 2500.0f;
    public Text SwipeLeft;
    public Text SwipeRight;
    public Text SwipeUp;
    public Text SwipeDown;
    
    private bool isFiring = false;

	// Use this for initialization
	void Start ()
    {
        var swipeController = gameObject.GetComponent<SwipeControl>();
        swipeController.SwipeLeft += SwipeController_SwipeLeft;
        swipeController.SwipeRight += SwipeController_SwipeRight;
        swipeController.SwipeUp += SwipeController_SwipeUp;
        swipeController.SwipeDown += SwipeController_SwipeDown;
    }

    void OnDestroy()
    {
        var swipeController = gameObject.GetComponent<SwipeControl>();
        swipeController.SwipeLeft -= SwipeController_SwipeLeft;
        swipeController.SwipeRight -= SwipeController_SwipeRight;
        swipeController.SwipeUp -= SwipeController_SwipeUp;
        swipeController.SwipeDown -= SwipeController_SwipeDown;
    }

    private void SwipeController_SwipeDown(float distance)
    {
        SwipeLeft.text = "0";
        SwipeRight.text = "0";
        SwipeUp.text = "0";
        SwipeDown.text = distance.ToString();
    }

    private void SwipeController_SwipeUp(float distance)
    {
        SwipeLeft.text = "0";
        SwipeRight.text = "0";
        SwipeUp.text = distance.ToString();
        SwipeDown.text = "0";
    }

    private void SwipeController_SwipeRight(float distance)
    {
        SwipeLeft.text = "0";
        SwipeRight.text = distance.ToString();
        SwipeUp.text = "0";
        SwipeDown.text = "0";
    }

    private void SwipeController_SwipeLeft(float distance)
    {
        SwipeLeft.text = distance.ToString();
        SwipeRight.text = "0";
        SwipeUp.text = "0";
        SwipeDown.text = "0";
    }

    // Update is called once per frame
    void Update ()
    {
	    if(Input.GetMouseButton(0))
        {
            if(!isFiring)
            {
                shootBall();
            }
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }
	}

    void shootBall()
    {
        var newBall = Instantiate(ball);
        newBall.transform.position = gameObject.transform.position;
        Vector3 shoot = (reticle.transform.position - gameObject.transform.position).normalized;
        newBall.GetComponent<Rigidbody>().AddForce(shoot * 1500.0f);
    }
}
