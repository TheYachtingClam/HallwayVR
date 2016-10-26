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
#if UNITY_ANDROID
        var swipeController = gameObject.GetComponent<SwipeControl>();
        swipeController.SwipeLeft += SwipeController_SwipeLeft;
        swipeController.SwipeRight += SwipeController_SwipeRight;
        swipeController.SwipeUp += SwipeController_SwipeUp;
        swipeController.SwipeDown += SwipeController_SwipeDown;
#else 
        var clickController = gameObject.GetComponent<ClickTimeControl>();
        clickController.Click += SwipeController_SwipeLeft;
#endif
    }

    void OnDestroy()
    {
#if UNITY_ANDROID
        var swipeController = gameObject.GetComponent<SwipeControl>();
        swipeController.SwipeLeft -= SwipeController_SwipeLeft;
        swipeController.SwipeRight -= SwipeController_SwipeRight;
        swipeController.SwipeUp -= SwipeController_SwipeUp;
        swipeController.SwipeDown -= SwipeController_SwipeDown;
#else
        var clickController = gameObject.GetComponent<ClickTimeControl>();
        clickController.Click -= SwipeController_SwipeLeft;
#endif
    }

    private void SwipeController_SwipeDown(float percentage)
    {
        SwipeLeft.text = "0";
        SwipeRight.text = "0";
        SwipeUp.text = "0";
        SwipeDown.text = percentage.ToString();
    }

    private void SwipeController_SwipeUp(float percentage)
    {
        SwipeLeft.text = "0";
        SwipeRight.text = "0";
        SwipeUp.text = percentage.ToString();
        SwipeDown.text = "0";
    }

    private void SwipeController_SwipeRight(float percentage)
    {
        SwipeLeft.text = "0";
        SwipeRight.text = percentage.ToString();
        SwipeUp.text = "0";
        SwipeDown.text = "0";
    }

    private void SwipeController_SwipeLeft(float percentage)
    {
        SwipeLeft.text = percentage.ToString();
        SwipeRight.text = "0";
        SwipeUp.text = "0";
        SwipeDown.text = "0";
        
        shootBall(lowSpeed + (percentage * (highSpeed - lowSpeed)));
    }

    // Update is called once per frame
    void Update ()
    {

	}

    void shootBall(float speed)
    {
        var newBall = Instantiate(ball);
        newBall.transform.position = gameObject.transform.position;
        Vector3 shoot = (reticle.transform.position - gameObject.transform.position).normalized;
        newBall.GetComponent<Rigidbody>().AddForce(shoot * speed);
    }
}
