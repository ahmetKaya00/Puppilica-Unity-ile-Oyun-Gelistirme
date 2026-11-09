using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
public class TouchController : MonoBehaviour {

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    public void OnDisable() {
        EnhancedTouchSupport.Disable();
    }
    
    void Update()
    {
        if (Touch.activeTouches.Count > 0) {
            Touch touch = Touch.activeTouches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Touch Began at: " + touch.screenPosition);
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Touch Moved to: " + touch.screenPosition);
                    break;
                case TouchPhase.Stationary:
                    Debug.Log("Touch Stationary at: " + touch.screenPosition);
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touch Ended at: " + touch.screenPosition);
                    break;
                case TouchPhase.Canceled:
                    Debug.Log("Touch Canceled at: " + touch.screenPosition);
                    break;

            }

            Vector2 touchPos = touch.screenPosition;

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            if(touchPos.x < screenWidth / 2)
            {
                Debug.Log("Touch is on the left side of the screen.");
            }
            else
            {
                Debug.Log("Touch is on the right side of the screen.");
            }

            if (touchPos.y < screenHeight / 2)
            {
                Debug.Log("Touch is on the bottom half of the screen.");
            }
            else
            {
                Debug.Log("Touch is on the top half of the screen.");
            }
        }
    }
}
