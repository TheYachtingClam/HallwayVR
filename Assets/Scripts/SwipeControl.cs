using System;
using UnityEngine;
using System.Collections;

public class SwipeControl : MonoBehaviour
{
    // After how long swipe should detect
    public float _swipeTolerance = 20;

    // Keeping initial position of finger/mouse
    Vector3 _initialPosition = Vector3.zero;

    // Will be calculate and compare with _swipeTolerance at Runtime
    float _swipeDistance = 0;

    // Determine swipe direction
    bool _swipedHorizontal = false;

    // Change in finger X position
    float _dx = 0;

    // Change in finger Y position
    float _dy = 0;

    public delegate void SwipeHandler(float distance);
    public event SwipeHandler SwipeLeft;
    public event SwipeHandler SwipeRight;
    public event SwipeHandler SwipeUp;
    public event SwipeHandler SwipeDown;

    void Awake()
    {
        // This will keep alive this GameObject through out all the scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        // Finger/click Start
        if (Input.GetMouseButtonDown(0))
        {

            // Saving initial touch/click position
            _initialPosition = Input.mousePosition;

            // Finger/click drag
        }
        else if (Input.GetMouseButton(0))
        {

            // Calculating change in X between initial and current X position
            _dx = _initialPosition.x - Input.mousePosition.x;

            // Calculating change in Y between initial and current Y position
            _dy = _initialPosition.y - Input.mousePosition.y;

            // Calculating swipe distance because it may be too short and should not execute
            _swipeDistance = Mathf.Sqrt(Mathf.Pow(_dx, 2) + Mathf.Pow(_dy, 2));

            // If change in X is greater than change in Y that means user swiped Horizontally
            _swipedHorizontal = Mathf.Abs(_dx) > Mathf.Abs(_dy);

            //Finger/click lift up.
        }
        else if (Input.GetMouseButtonUp(0))
        {
            print("Swipe distance: " + _swipeDistance);
            // Checking finger/click travel distance to the tolerance value.
            if (_swipeDistance > _swipeTolerance) {

                // Swiped left. As it is Horizontal and change in X is positive. Example 0 - (-5) would be +10 or 10
                if (_swipedHorizontal && _dx > 0) {
                    print("Swiped left");
                    if(SwipeLeft != null)
                        SwipeLeft(_swipeDistance);

                    // Swiped Right
                } else if (_swipedHorizontal && _dx < 0) {
                    print("Swiped Right");
                    if (SwipeRight != null)
                        SwipeRight(_swipeDistance);

                    // Swiped Down
                } else if (!_swipedHorizontal && _dy > 0) {
                    print("Swiped Down");
                    if (SwipeDown != null)
                        SwipeDown(_swipeDistance);

                    // Swiped Up
                } else if (!_swipedHorizontal && _dy < 0) {
                    print("Swiped Up");
                    if (SwipeUp != null)
                        SwipeUp(_swipeDistance);
                }
            }

            // Resetting values
            _dx = 0;
            _dy = 0;
            _initialPosition = Vector3.zero;
        }
    }

}