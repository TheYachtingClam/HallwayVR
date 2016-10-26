using UnityEngine;
using System.Collections;

public class BallShooter : MonoBehaviour {

    public GameObject ball;
    public GameObject reticle;
    public float speed = 500.0f;
    
    private bool isFiring = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
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
        newBall.GetComponent<Rigidbody>().AddForce(shoot * speed);
    }
}
