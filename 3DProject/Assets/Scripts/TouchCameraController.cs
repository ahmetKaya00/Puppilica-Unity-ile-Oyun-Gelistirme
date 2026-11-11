using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchCameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Transform _cameraBody;
    [SerializeField] private float _touchSensitivity = 0.15f;

    [SerializeField] private float _upperLookLimit = 80f;
    [SerializeField] private float _lowerLookLimit = -80f;

    private float _cameraVerticalRotation = 0f;
    private int _cameraFingerId = -1;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if(Touchscreen.current != null)
        {
            HandleTouchScreenLook();
        }

#if UNITY_EDITOR
        HandleMouseLook();
#endif
    }

    private void HandleTouchScreenLook()
    {
        var acticaTouches = Touch.activeTouches;

        if(acticaTouches.Count == 0)
        {
            _cameraFingerId = -1;
            return;
        }

        foreach(var touch in acticaTouches)
        {
            if(touch.phase == UnityEngine.InputSystem.TouchPhase.Began) {
                if(touch.screenPosition.x > (Screen.width / 2)&& !EventSystem.current.IsPointerOverGameObject(touch.touchId))
                {
                    if(_cameraFingerId == -1)
                    {
                        _cameraFingerId = touch.touchId;
                    }
                }
            }
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                if(touch.touchId == _cameraFingerId)
                {
                    RotateView(touch.delta.x,touch.delta.y);

                }
            }
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                if (touch.touchId == _cameraFingerId)
                {
                    _cameraFingerId = -1;
                }
            }

        }
    }

    private void HandleMouseLook()
    {
        if(Mouse.current == null) return;

        if(Mouse.current.leftButton.isPressed && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            RotateView(mouseDelta.x, mouseDelta.y);
        }
    }

    private void RotateView(float deltaX, float deltaY)
    {
        if(_playerBody != null)
        {
            _playerBody.Rotate(Vector3.up * (deltaX * _touchSensitivity));
        }

        _cameraVerticalRotation -= deltaY * _touchSensitivity;

        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, _lowerLookLimit, _upperLookLimit);

        if(_cameraBody != null)
        {
            _cameraBody.localRotation = Quaternion.Euler(_cameraVerticalRotation, 0f, 0f);
        }
    }

}
